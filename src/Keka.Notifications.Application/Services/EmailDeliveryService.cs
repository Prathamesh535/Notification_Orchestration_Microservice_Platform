// -----------------------------------------------------------------------
// <copyright file="EmailDeliveryService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using ApplicationException = Keka.Notifications.Application.Exceptions.ApplicationException;

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents email delivery service.
/// </summary>
public class EmailDeliveryService : IEmailDeliveryService
{
    private const int MaxConsecutiveBlockCount = 4;
    private const int BlockJobThreshold = 2;
    private readonly IEmailDeliveryHistoryRepository emailDeliveryHistoryRepository;
    private readonly IEmailRepository emailRepository;
    private readonly IEmailAttachmentRepository emailAttachmentRepository;
    private readonly IEmailDeliveryConverter emailDeliveryConverter;
    private readonly IEmailStatusRepository emailStatusRepository;
    private readonly IDateTimeService dateTimeService;
    private readonly IMapper mapper;
    private readonly IAppContext appContext;
    private readonly IEmailProvider emailProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailDeliveryService"/> class.
    /// </summary>
    /// <param name="mapper">The maper instance.</param>
    /// <param name="appContext">The app context.</param>
    /// <param name="emailRepository">The email repository instance.</param>
    /// <param name="emailAttachmentRepository">The email attachment repository instance.</param>
    /// <param name="emailDeliveryHistoryRepository">The email delivery repository instance.</param>
    /// <param name="emailDeliveryConverter">The email delivery converter instance.</param>
    /// <param name="emailStatusRepository">The email status repository instance.</param>
    /// <param name="dateTimeService">The date time service instance.</param>
    /// <param name="emailProvider">The email provider instance.</param>
    public EmailDeliveryService(IMapper mapper, IAppContext appContext, IEmailRepository emailRepository, IEmailDeliveryHistoryRepository emailDeliveryHistoryRepository, IEmailDeliveryConverter emailDeliveryConverter, IEmailStatusRepository emailStatusRepository, IEmailAttachmentRepository emailAttachmentRepository, IDateTimeService dateTimeService, IEmailProvider emailProvider)
    {
        this.mapper = mapper;
        this.appContext = appContext;
        this.emailRepository = emailRepository;
        this.emailDeliveryHistoryRepository = emailDeliveryHistoryRepository;
        this.emailDeliveryConverter = emailDeliveryConverter;
        this.emailStatusRepository = emailStatusRepository;
        this.emailAttachmentRepository = emailAttachmentRepository;
        this.dateTimeService = dateTimeService;
        this.emailProvider = emailProvider;
    }

    /// <inheritdoc/>
    public async Task SyncEmailAsync(SyncEmailEvent syncEmailEvent)
    {
        // Get email
        Email emailMessage = await this.emailRepository.GetEmailAsync(syncEmailEvent.EmailId);
        emailMessage.SentOn = syncEmailEvent.CreationDate;

        // Insert email status records (or) increment email sent count.
        await this.AddOrUpdateEmailStatusRecordsAsync(emailMessage);

        // Sync email record to table storage
        var emailHistoryRecords = this.emailDeliveryConverter.ConvertToEmailHistory(syncEmailEvent, emailMessage);
        await this.UpsertEmailHistoryAsync(emailHistoryRecords);

        // Delete the email record.
        await this.emailRepository.DeleteEmailAsync(syncEmailEvent.EmailId);

        // Delete the email attachments.
        if (emailMessage.Attachments.Any())
        {
            await this.emailAttachmentRepository.DeleteEmailAttachmentAsync(emailMessage.Attachments.Select(c => c.FilePath).ToList());
        }
    }

    /// <inheritdoc/>
    public async Task SyncEmailDeliveryEventsAsync(List<SqsEventDto> sqsEvents)
    {
        // Convert
        List<EmailDeliveryRawData> rawEmailDeliveryEvents;
        var emailHistoryRecords = this.emailDeliveryConverter.ConvertToEmailHistory(sqsEvents, out rawEmailDeliveryEvents);

        // Validate
        if (emailHistoryRecords is null || emailHistoryRecords.Count == 0)
        {
            throw new ArgumentException("Unable to convert events to email history records.");
        }

        // Get the first email delivery history and validate
        var firstEmailDeliveryHistory = emailHistoryRecords.FirstOrDefault();
        if (firstEmailDeliveryHistory is null || !firstEmailDeliveryHistory.TenantId.HasValue)
        {
            throw new ArgumentException("TenantId is missing in delivery events.");
        }

        this.appContext.TenantId = emailHistoryRecords.FirstOrDefault().TenantId.Value;

        // Get events related email status records.
        var emailIds = emailHistoryRecords.Select(e => e.ToEmail).ToList();
        var allEmailStatusRecords = await this.emailStatusRepository.GetEmailStatusRecordsByEmailAsync(emailIds);

        // Ignore duplicate delivery events which are already recived as bounce.
        RemoveDuplicateEmailHistoryRecords(ref emailHistoryRecords, allEmailStatusRecords);

        // Update data to table storage
        await this.UpsertEmailRawEventsAsync(rawEmailDeliveryEvents);
        if (emailHistoryRecords.Count == 0)
        {
            return;
        }

        await this.UpsertEmailHistoryAsync(emailHistoryRecords);

        // Update email status records
        var blockEmailRequests = new List<BlockEmailRequest>();
        foreach (var emailHistoryRecord in emailHistoryRecords)
        {
            var existingEmailStatusRecord = allEmailStatusRecords.Find(e => e.Email.Equals(emailHistoryRecord.ToEmail, StringComparison.InvariantCultureIgnoreCase));
            var (emailStatusRecord, isNewlyBlocked) = this.IdentifyEmailStatusChanges(emailHistoryRecord, existingEmailStatusRecord);

            if (existingEmailStatusRecord is null)
            {
                await this.emailStatusRepository.AddEmailStatusRecordAsync(emailStatusRecord);
            }
            else
            {
                await this.emailStatusRepository.UpdateEmailStatusRecordAsync(emailStatusRecord);
            }

            if (isNewlyBlocked)
            {
                blockEmailRequests.Add(new BlockEmailRequest()
                {
                    Email = emailHistoryRecord.ToEmail,
                    Reason = emailHistoryRecord.DeliveryStatus.ToString(),
                });
            }
        }

        // Block emails in amazon
        await this.BlockEmailsAsync(blockEmailRequests);
    }

    /// <inheritdoc/>
    public async Task<PagedResponse<EmailDeliveryHistoryDto>> GetEmailDeliveryHistoryAsync(GetEmailHistoryRequest getEmailHistoryRequest)
    {
        var emailHistoryRecords = await this.emailDeliveryHistoryRepository.GetEmailDeliveryHistoryAsync(getEmailHistoryRequest);
        var pagedEmailHistoryDtos = new PagedResponse<EmailDeliveryHistoryDto>
        {
            Items = this.mapper.Map<List<EmailDeliveryHistoryDto>>(emailHistoryRecords.Items),
            ContinuationToken = emailHistoryRecords.ContinuationToken,
        };
        return pagedEmailHistoryDtos;
    }

    /// <inheritdoc/>
    public async Task<List<EmailDeliveryRawDataBasicDetailsDto>> GetEmailDeliveryDataByMessageIdAsync(string messageId)
    {
        return this.mapper.Map<List<EmailDeliveryRawDataBasicDetailsDto>>(await this.emailDeliveryHistoryRepository.GetRawEmailDeliveryHistoryAsync(messageId));
    }

    private static void RemoveDuplicateEmailHistoryRecords(ref List<EmailDeliveryHistory> emailHistoryRecords, List<EmailStatus> emailStatusRecords)
    {
        var duplicateEmailHistoryRecords = emailHistoryRecords.Where(emailHistoryRecord => emailHistoryRecord.DeliveryStatus.Equals(EmailDeliveryStatus.Delivery) && emailHistoryRecord.ExternalId.Equals(emailStatusRecords.Find(emailStatusRecord => emailStatusRecord.Email.Equals(emailHistoryRecord.ToEmail, StringComparison.InvariantCultureIgnoreCase))?.PreviousStateDetails?.MessageExternalId)).ToList();
        emailHistoryRecords = emailHistoryRecords.Except(duplicateEmailHistoryRecords).ToList();
    }

    private static List<string> ExtractRecipientEmailIds(EmailRecipients emailRecipients)
    {
        List<string> emailIds = new List<string>();
        if (emailRecipients is null)
        {
            return emailIds;
        }

        emailIds.AddRange(emailRecipients.To.Select(x => x.Address).ToList());
        emailIds.AddRange(emailRecipients.Cc.Select(x => x.Address).ToList());
        emailIds.AddRange(emailRecipients.Bcc.Select(x => x.Address).ToList());
        return emailIds;
    }

    private async Task UpsertEmailHistoryAsync(List<EmailDeliveryHistory> emailDeliveryReports)
    {
        await this.emailDeliveryHistoryRepository.UpsertEmailHistoryAsync(emailDeliveryReports);
    }

    private async Task UpsertEmailRawEventsAsync(List<EmailDeliveryRawData> emailDeliveryRawEvents)
    {
        await this.emailDeliveryHistoryRepository.UpsertEmailDeliveryRawDataAsync(emailDeliveryRawEvents);
    }

    private async Task AddOrUpdateEmailStatusRecordsAsync(Email email)
    {
        var emailIds = ExtractRecipientEmailIds(email.Recipients);
        var dbEmailStatusRecords = await this.emailStatusRepository.GetEmailStatusIdByEmailAsync(emailIds);

        // Increment no of emails sent for existing email status records.
        foreach (var emailStatus in dbEmailStatusRecords)
        {
            emailStatus.LastEmailSentOn = email.SentOn;
            await this.emailStatusRepository.IncrementEmailSentCountAsync(emailStatus);
        }

        // Find new emails and create email status record.
        var dbEmailIds = dbEmailStatusRecords.Select(x => x.Email).ToList();
        List<string> newEmailIds = emailIds.Except(dbEmailIds, StringComparer.InvariantCultureIgnoreCase).ToList();
        foreach (var emailId in newEmailIds)
        {
            var emailStatusRecord = new EmailStatus()
            {
                Email = emailId,
                LastEmailSentOn = email.SentOn,
                NoOfEmailsSent = 1,
            };
            await this.emailStatusRepository.AddEmailStatusRecordAsync(emailStatusRecord);
        }
    }

    private (EmailStatus emailStatus, bool isEmailNewlyBlocked) IdentifyEmailStatusChanges(EmailDeliveryHistory emailDeliveryHistory, EmailStatus emailStatus)
    {
        emailStatus = emailStatus ?? new EmailStatus();

        var currentTime = this.dateTimeService.GetCurrentTimeUtc();
        emailStatus.Email = emailDeliveryHistory.ToEmail;

        emailStatus.PreviousStateDetails = emailStatus.PreviousStateDetails ?? new EmailStatusPreviousStateDetails();
        bool isEmailNewlyBlocked = false;

        switch (emailDeliveryHistory.DeliveryStatus)
        {
            case EmailDeliveryStatus.Delivery:

                emailStatus.PreviousStateDetails.LastEmailDeliveredOn = emailStatus.LastEmailDeliveredOn;
                emailStatus.PreviousStateDetails.ConsecutiveBlockCount = emailStatus.ConsecutiveBlockCount;
                emailStatus.LastEmailDeliveredOn = currentTime;
                emailStatus.ConsecutiveBlockCount = 0;
                emailStatus.IsBlocked = false;
                emailStatus.BlockedUntil = null;
                break;

            case EmailDeliveryStatus.Bounce:
                if (emailDeliveryHistory.FailedReason.HasValue && emailDeliveryHistory.FailedReason.Equals(EmailFailedReason.AccountOnSuppressionList))
                {
                    emailStatus.LastEmailFailedOn = currentTime;
                    emailStatus.NoOfEmailsFailed++;
                    emailStatus.LastEmailFailedReason = (short)emailDeliveryHistory.FailedReason.Value;
                    break;
                }

                if (emailDeliveryHistory.ExternalId.Equals(emailStatus.PreviousStateDetails?.MessageExternalId))
                {
                    emailStatus.ConsecutiveBlockCount = emailStatus.PreviousStateDetails.ConsecutiveBlockCount;
                    emailStatus.LastEmailDeliveredOn = emailStatus.PreviousStateDetails.LastEmailDeliveredOn;
                }

                if (!emailStatus.IsBlocked)
                {
                    isEmailNewlyBlocked = true;
                }

                emailStatus.LastEmailFailedOn = currentTime;
                emailStatus.IsBlocked = true;
                emailStatus.LastBlockedOn = this.dateTimeService.GetCurrentTimeUtc();
                emailStatus.NoOfEmailsFailed++;
                emailStatus.BlockedUntil = emailStatus.ConsecutiveBlockCount switch
                {
                    0 => currentTime.AddDays(1).Date,
                    1 => currentTime.AddDays(3).Date,
                    2 => currentTime.AddDays(5).Date,
                    _ => null,
                };
                if (emailStatus.ConsecutiveBlockCount < MaxConsecutiveBlockCount)
                {
                    emailStatus.ConsecutiveBlockCount++;
                }

                if (emailDeliveryHistory.FailedReason.HasValue)
                {
                    emailStatus.LastEmailFailedReason = (short)emailDeliveryHistory.FailedReason.Value;
                }

                break;

            case EmailDeliveryStatus.Complaint:
                if (!emailStatus.IsBlocked)
                {
                    isEmailNewlyBlocked = true;
                }

                emailStatus.LastEmailFailedOn = currentTime;
                emailStatus.NoOfEmailsFailed++;
                emailStatus.IsBlocked = true;
                emailStatus.LastBlockedOn = this.dateTimeService.GetCurrentTimeUtc();
                break;
        }

        emailStatus.PreviousStateDetails.MessageExternalId = emailDeliveryHistory.ExternalId;

        return (emailStatus, isEmailNewlyBlocked);
    }

    private async Task BlockEmailsAsync(List<BlockEmailRequest> blockEmailRequests)
    {
        if (blockEmailRequests is null || blockEmailRequests.Count == 0)
        {
            return;
        }

        // If requests are greater than threshold, blocking using job
        if (blockEmailRequests.Count > BlockJobThreshold)
        {
            await this.emailProvider.BlockAsync(blockEmailRequests);
            return;
        }

        // If requests are less than threshold, blocking one by one
        foreach (var request in blockEmailRequests)
        {
            var blockResponse = await this.emailProvider.BlockAsync(request);
            if (!blockResponse.Success)
            {
                throw new ApplicationException(ErrorCode.UNKNOWN, $"Error while blocking email {request.Email} : {blockResponse.ErrorMessage}");
            }
        }
    }
}