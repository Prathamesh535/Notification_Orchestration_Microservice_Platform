// -----------------------------------------------------------------------
// <copyright file="EmailDeliveryConverter.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Helpers;

/// <summary>
/// Represents email delivery converter class.
/// </summary>
public partial class EmailDeliveryConverter : IEmailDeliveryConverter
{
    private static readonly Dictionary<string, EmailDeliveryStatus> DeliveryStatusLookup = new ()
    {
        { Constants.Delivery, EmailDeliveryStatus.Delivery },
        { Constants.Bounce, EmailDeliveryStatus.Bounce },
        { Constants.Complaint, EmailDeliveryStatus.Complaint },
    };

    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly IDateTimeService dateTimeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailDeliveryConverter"/> class.
    /// </summary>
    /// <param name="dateTimeService">Instance of datetime service.</param>
    public EmailDeliveryConverter(IDateTimeService dateTimeService)
    {
        this.dateTimeService = dateTimeService;
    }

    /// <inheritdoc/>
    public List<EmailDeliveryHistory> ConvertToEmailHistory(List<SqsEventDto> sqsEventDtos, out List<EmailDeliveryRawData> emailDeliveryRawDataRecords)
    {
        var emailHistoryRecords = new List<EmailDeliveryHistory>();
        emailDeliveryRawDataRecords = new List<EmailDeliveryRawData>();
        EmailDeliveryRawData emailDeliveryRawData = null;
        foreach (var sqsEventDto in sqsEventDtos)
        {
            emailDeliveryRawData = this.ConvertToEmailDeliveryRawData(sqsEventDto);
            var sqsEventBodyDto = JsonSerializer.Deserialize<SqsEventBodyDto>(sqsEventDto.Body, JsonSerializerOptions);
            if (sqsEventBodyDto is not null && sqsEventBodyDto.Message is not null)
            {
                var sqsMessage = JsonSerializer.Deserialize<SqsMessageDto>(sqsEventBodyDto.Message, JsonSerializerOptions);
                if (sqsMessage is not null)
                {
                    emailDeliveryRawData.ExternalId = sqsMessage.Mail.MessageId;
                    var tenantId = ExtractTenantId(sqsMessage.Mail.Tags);
                    emailHistoryRecords.AddRange(this.ConvertToEmailHistories(sqsMessage, tenantId));
                }
            }

            emailDeliveryRawDataRecords.Add(emailDeliveryRawData);
        }

        return emailHistoryRecords;
    }

    /// <inheritdoc/>
    public List<EmailDeliveryHistory> ConvertToEmailHistory(SyncEmailEvent syncEmailEvent, Email email)
    {
        List<EmailDeliveryHistory> emailHistoryRecords = new List<EmailDeliveryHistory>();
        EmailDeliveryHistory recipient = null;
        List<Core.Models.EmailMessages.EmailAddress> emailAddresses =
        [
            .. email.Recipients.To,
            .. email.Recipients.Cc,
            .. email.Recipients.Bcc,
        ];
        foreach (var emailAddress in emailAddresses)
        {
            recipient = this.ConvertToEmailHistory(syncEmailEvent, email, emailAddress);
            emailHistoryRecords.Add(recipient);
        }

        return emailHistoryRecords;
    }

    private static Guid? ExtractTenantId(Dictionary<string, List<string>> tags)
    {
        if (tags is not null && tags.TryGetValue("TenantId", out List<string> tenantTag))
        {
            string tenantId = tenantTag.FirstOrDefault();
            if (Guid.TryParse(tenantId, out Guid tenantGuid))
            {
                return tenantGuid;
            }
        }

        return null;
    }

    private static EmailDeliveryStatus ExtractEventType(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            if (data.Contains(Constants.Delivery))
            {
                return EmailDeliveryStatus.Delivery;
            }
            else if (data.Contains(Constants.Bounce))
            {
                return EmailDeliveryStatus.Bounce;
            }
            else if (data.Contains(Constants.Complaint))
            {
                return EmailDeliveryStatus.Complaint;
            }
        }

        return EmailDeliveryStatus.None;
    }

    private static EmailFailedReason? ExtractEmailFailedReason(EmailDeliveryStatus emailDeliveryStatus, string reason, string subReason)
    {
        reason = (reason ?? string.Empty).ToLowerInvariant();
        subReason = (subReason ?? string.Empty).ToLowerInvariant();
        switch (emailDeliveryStatus)
        {
            case EmailDeliveryStatus.Delivery:
                return null;
            case EmailDeliveryStatus.Bounce:
                switch (reason)
                {
                    case Constants.BounceTransient:
                        if (string.Equals(subReason, Constants.BounceAttachmentRejected, StringComparison.OrdinalIgnoreCase))
                        {
                            return EmailFailedReason.BounceAttachmentRejected;
                        }

                        return EmailFailedReason.BounceTransient;
                    case Constants.BounceUndetermined:
                    case Constants.BouncePermanent:
                        if (string.Equals(subReason, Constants.AccountOnSupressionList, StringComparison.OrdinalIgnoreCase))
                        {
                            return EmailFailedReason.AccountOnSuppressionList;
                        }

                        return EmailFailedReason.BouncePermanent;

                    default:
                        return EmailFailedReason.BouncePermanent;
                }

            case EmailDeliveryStatus.Complaint:
                return EmailFailedReason.EmailComplaint;
            default:
                return EmailFailedReason.Unknown;
        }
    }

    /// <summary>
    /// This function extracts email from given input.
    /// </summary>
    /// <param name="input">Input.</param>
    /// <returns>Email.</returns>
    private static string ExtractEmail(string input)
    {
        // From amazon sometimes we are getting email in this format: John Doe <johndoe@abc.com>'.
        int startIndex = input.IndexOf('<');
        int endIndex = input.IndexOf('>');
        if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
        {
            input = input.Substring(startIndex + 1, endIndex - startIndex - 1);
        }

        return input;
    }

    private List<EmailDeliveryHistory> ConvertToEmailHistories(SqsMessageDto sqsMessage, Guid? tenantId)
    {
        var emailHistoryRecords = new List<EmailDeliveryHistory>();
        if (sqsMessage is null)
        {
            return emailHistoryRecords;
        }

        switch (sqsMessage.GetEventType())
        {
            case Constants.Delivery:
                emailHistoryRecords.AddRange(this.ProcessDeliveryEvent(sqsMessage, tenantId));
                break;

            case Constants.Complaint:
                emailHistoryRecords.AddRange(this.ProcessComplaintEvent(sqsMessage, tenantId));
                break;

            case Constants.Bounce:
                emailHistoryRecords.AddRange(this.ProcessBounceEvent(sqsMessage, tenantId));
                break;

            default:
                break;
        }

        return emailHistoryRecords;
    }

    private IEnumerable<EmailDeliveryHistory> ProcessDeliveryEvent(SqsMessageDto sqsMessage, Guid? tenantId)
    {
        return sqsMessage.Delivery?.Recipients?.Select(recipient => this.BuildEmailDeliveryHistory(sqsMessage, recipient, tenantId)) ?? Enumerable.Empty<EmailDeliveryHistory>();
    }

    private IEnumerable<EmailDeliveryHistory> ProcessComplaintEvent(SqsMessageDto sqsMessage, Guid? tenantId)
    {
        return sqsMessage.Complaint?.ComplainedRecipients?.Select(recipient => this.BuildEmailDeliveryHistory(sqsMessage, recipient.EmailAddress, tenantId, sqsMessage.Complaint.ComplaintFeedbackType)) ?? Enumerable.Empty<EmailDeliveryHistory>();
    }

    private IEnumerable<EmailDeliveryHistory> ProcessBounceEvent(SqsMessageDto sqsMessage, Guid? tenantId)
    {
        return sqsMessage.Bounce?.BouncedRecipients?.Select(recipient => this.BuildEmailDeliveryHistory(sqsMessage, recipient.EmailAddress, tenantId, sqsMessage.Bounce.BounceType, sqsMessage.Bounce.BounceSubType)) ?? Enumerable.Empty<EmailDeliveryHistory>();
    }

    private EmailDeliveryHistory BuildEmailDeliveryHistory(SqsMessageDto sqsMessage, string recipient, Guid? tenantId, string reason = null, string subReason = null)
    {
        var deliveryStatus = DeliveryStatusLookup.TryGetValue(sqsMessage.GetEventType(), out var status) ? status : EmailDeliveryStatus.None;
        return new EmailDeliveryHistory
        {
            ToEmail = ExtractEmail(recipient),
            ExternalId = sqsMessage.Mail.MessageId,
            FromEmail = ExtractEmail(sqsMessage.Mail.Source),
            DeliveryStatus = deliveryStatus,
            FailedReason = ExtractEmailFailedReason(deliveryStatus, reason, subReason),
            UpdatedOn = this.dateTimeService.GetCurrentTimeUtc(),
            TenantId = tenantId,
        };
    }

    private EmailDeliveryHistory ConvertToEmailHistory(SyncEmailEvent syncEmailEvent, Email email, Core.Models.EmailMessages.EmailAddress emailAddress)
    {
        EmailDeliveryHistory emailDeliveryReport = new EmailDeliveryHistory();
        emailDeliveryReport.FromEmail = email.From.Address;
        emailDeliveryReport.ToEmail = emailAddress.Address;
        emailDeliveryReport.ExternalId = syncEmailEvent.ExternalId;
        emailDeliveryReport.Subject = email.Content.Subject;
        emailDeliveryReport.UpdatedOn = this.dateTimeService.GetCurrentTimeUtc();
        emailDeliveryReport.UserId = syncEmailEvent.UserId;
        emailDeliveryReport.TenantId = syncEmailEvent.TenantId;
        emailDeliveryReport.FailedReason = null;
        emailDeliveryReport.DeliveryStatus = null;
        emailDeliveryReport.SentOn = email.SentOn;
        emailDeliveryReport.HasAttachment = email.Attachments.Any();
        return emailDeliveryReport;
    }

    private EmailDeliveryRawData ConvertToEmailDeliveryRawData(SqsEventDto sqsEventDto)
    {
        return new EmailDeliveryRawData()
        {
            DeliveryStatus = ExtractEventType(sqsEventDto.Body),
            ExternalId = sqsEventDto.MessageId,
            Md5Checksum = sqsEventDto.Md5OfBody,
            RawResponse = sqsEventDto.Body,
            TimeStamp = this.dateTimeService.GetCurrentTimeUtc(),
        };
    }

    private static class Constants
    {
        public const string Delivery = "delivery";
        public const string Complaint = "complaint";
        public const string Bounce = "bounce";
        public const string BounceTransient = "transient";
        public const string BounceUndetermined = "undetermined";
        public const string BouncePermanent = "permanent";
        public const string AccountOnSupressionList = "onaccountsuppressionlist";
        public const string BounceAttachmentRejected = "attachmentRejected";
    }
}