// -----------------------------------------------------------------------
// <copyright file="EmailSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents an email sender service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EmailSenderService"/> class.
/// </remarks>
/// <param name="logger">The logger service.</param>
/// <param name="emailProvider">The email provider instance.</param>
/// <param name="appContext">The app context.</param>
/// <param name="eventBus">The email bus instance.</param>
/// <param name="emailRepository">The email repository instance.</param>
/// <param name="emailAttachmentRepository">The email attachment repository instance.</param>
public class EmailSenderService(ILogger<EmailSenderService> logger, IEmailProvider emailProvider, IAppContext appContext, IEventBus eventBus, IEmailRepository emailRepository, IEmailAttachmentRepository emailAttachmentRepository)
    : IEmailSenderService
{
    /// <inheritdoc/>
    public async Task SendEmailAsync(SendEmailEvent sendEmailEvent)
    {
        // Get email.
        var emailMessage = await emailRepository.GetEmailAsync(sendEmailEvent.EmailId);
        if (emailMessage is null)
        {
            logger.LogInformation("Email Record is not found with given Id {EmailId}", sendEmailEvent.EmailId);
            return;
        }

        // Send email.
        EmailSendResponse emailSendResponse = await this.SendEmailAsync(emailMessage);

        try
        {
            // Added try catch only for this section because we don't want to re-try if email sending doesn't throw exception.
            // If email sending is success, push to table storage
            if (emailSendResponse.Status == NotificationStatus.Sent)
            {
                // Push sync email event to eventbus
                var syncEmailEvent = new SyncEmailEvent(appContext.TenantId, appContext.UserId, sendEmailEvent.EmailId, emailSendResponse.ExternalId);
                await eventBus.PublishAsync(syncEmailEvent);
            }
            else
            {
                var recipients = emailMessage.Recipients?.To?.ToJson();
                logger.LogError("Error from amazon while sending an email for message id: {emailId}, Recipients: {recipients}", sendEmailEvent.EmailId, recipients);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error after sending an email for message id: {emailId}. Message : {message}", sendEmailEvent.EmailId, ex.Message);
        }
    }

    private static Abstractions.Email.EmailAddress MapToEmailAddress(Core.Models.EmailMessages.EmailAddress emailAddress)
    {
        if (emailAddress is null)
        {
            return null;
        }

        return new Abstractions.Email.EmailAddress
        {
            Email = emailAddress.Address,
            DisplayName = emailAddress.DisplayName,
        };
    }

    private async Task<SendEmailRequest> MapToSendEmailRequest(Email email)
    {
        var sendEmailRequest = new SendEmailRequest();

        // Map From and To addresses
        sendEmailRequest.FromEmailAddress = MapToEmailAddress(email.From);

        if (email.Recipients != null)
        {
            sendEmailRequest.ToAddresses = email.Recipients?.To?.Select(MapToEmailAddress).ToList();
            sendEmailRequest.CcAddresses = email.Recipients?.Cc.Select(MapToEmailAddress).ToList();
            sendEmailRequest.BccAddresses = email.Recipients?.Bcc.Select(MapToEmailAddress).ToList();
        }

        // Map ReplyTo addresses
        sendEmailRequest.ReplyToAddresses = email.ReplyTo?.Select(MapToEmailAddress).ToList();

        // Map Subject and Body
        sendEmailRequest.Subject = email.Content?.Subject;
        var isBodyHtml = email.Content?.Html != null;
        sendEmailRequest.Body = isBodyHtml ? email.Content.Html : email.Content.PlainText;
        sendEmailRequest.IsBodyHtml = isBodyHtml;
        sendEmailRequest.Tags = new Dictionary<string, string>
        {
            { "TenantId", appContext.TenantId.ToString() },
        };

        // Map Attachments
        if (email.Attachments != null)
        {
            sendEmailRequest.Attachments = new List<Attachment>();
            foreach (var emailAttachment in email.Attachments)
            {
                sendEmailRequest.Attachments.Add(new Attachment
                {
                    Content = await emailAttachmentRepository.GetEmailAttachmentAsync(emailAttachment.FilePath),
                    Name = emailAttachment.Name,
                });
            }
        }

        return sendEmailRequest;
    }

    private async Task<EmailSendResponse> SendEmailAsync(Email emailMessage)
    {
        var sendEmailRequest = await this.MapToSendEmailRequest(emailMessage);
        SendEmailResponse response = await emailProvider.SendAsync(sendEmailRequest);
        var emailSentStatus = response is not null && response.Success ? NotificationStatus.Sent : NotificationStatus.Failed;

        return new EmailSendResponse(emailSentStatus, response?.ReferenceId);
    }
}