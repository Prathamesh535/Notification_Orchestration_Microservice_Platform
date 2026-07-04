// -----------------------------------------------------------------------
// <copyright file="EmailService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents an email service.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="appContext">The app context.</param>
/// <param name="eventBus">The event bus.</param>
/// <param name="templateHelper">The template helper.</param>
/// <param name="emailTemplateRepository">The email template repository instance.</param>
/// <param name="emailRepository">The email repository instance.</param>
/// <param name="emailRequestRepository">The email request repository instance.</param>
/// <param name="emailAttachmentRepository">The email attachment repository instance.</param>
public partial class EmailService(ILogger<EmailService> logger, IMapper mapper, IAppContext appContext, IEventBus eventBus, ITemplateHelper templateHelper, IEmailTemplateRepository emailTemplateRepository, IEmailRepository emailRepository, IEmailRequestRepository emailRequestRepository, IEmailAttachmentRepository emailAttachmentRepository)
    : IEmailService
{
    private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private const string EscapeCharactersPattern = @"[\t\n\r\a\b\f\v\\""]";
    private const string MultipleWhiteSpacePattern = @"\s+";

    /// <summary>
    /// Represents the template helper.
    /// </summary>
    private readonly ITemplateHelper templateHelper = templateHelper;

    /// <summary>
    /// Represents the email template repository.
    /// </summary>
    private readonly IEmailTemplateRepository emailTemplateRepository = emailTemplateRepository;

    /// <inheritdoc />
    public async Task<Guid> AddEmailRequestAsync(EmailRequestDto emailRequestDto)
    {
        string emailRequestId = string.Empty;
        try
        {
            // check for valid From email address and atleast one valid To email address.
            ErrorCode? errorCode;
            string errorMessage;
            if (!ValidateEmailRequest(emailRequestDto, out errorCode, out errorMessage))
            {
                throw new Exceptions.ApplicationException(errorCode.Value, errorMessage);
            }

            var emailRequest = mapper.Map<EmailRequest>(emailRequestDto);
            emailRequest.EmailRequestId = Guid.NewGuid();
            emailRequestId = emailRequest.EmailRequestId.ToString();

            emailRequest.Attachments = await this.SyncEmailAttachments(emailRequestDto);

            // Save the email message to the database
            emailRequestId = await emailRequestRepository.SaveEmailRequestAsync(emailRequest);

            // Publish the email message event to the event bus for processing in the background
            var emailPublishedEvent = new EmailRequestReceivedEvent(appContext.TenantId, appContext.UserId, emailRequestId);

            await eventBus.PublishAsync(emailPublishedEvent);

            return emailRequest.EmailRequestId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending email: Request id:{requestId}, Message:{message}", emailRequestId, ex.Message);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task EnrichEmailRequestAsync(string emailRequestId)
    {
        try
        {
            // Fetching email request from DB.
            var emailRequest = await emailRequestRepository.GetEmailRequestAsync(emailRequestId);
            EmailTemplate emailTemplate = null;

            // Checking weather email request has any template id.
            if (emailRequest.TemplateId.HasValue)
            {
                emailTemplate = await this.emailTemplateRepository.GetEmailTemplateAsync(emailRequest.TemplateId.Value);
            }

            // Compiling email template's subject and body if email template is not null.
            HandlebarsTemplate<object, object> compiledTemplateSubject = null;
            HandlebarsTemplate<object, object> compiledTemplateBody = null;
            if (emailTemplate is not null)
            {
                compiledTemplateSubject = this.templateHelper.CompileTemplate(emailTemplate.Subject);
                compiledTemplateBody = this.templateHelper.CompileTemplate(emailTemplate.Body);
            }

            // Building an email.
            List<string> invalidEmails = new ();
            var validReplyTo = FilterInvalidEmails(emailRequest.ReplyTo, ref invalidEmails);
            RemoveEscapeCharacters(validReplyTo);
            var builder = new EmailBuilder();
            var emailBuilder = builder
                    .StartNewEmail()
                    .SetFrom(emailRequest.From)
                    .SetReplyTo(validReplyTo)
                    .SetAttachments(emailRequest.Attachments)
                    .SetEmailRequestId(emailRequestId);
            if (invalidEmails.Count > 0)
            {
                logger.LogError("Ignoring invalid reply-to email addresses for email request {emailRequestId}: {invalidEmails}", emailRequest.EmailRequestId, string.Join(", ", invalidEmails.Distinct()));
            }

            foreach (var personalization in emailRequest.Personalization)
            {
                // Setting content for email if email template is not null
                if (emailTemplate is not null)
                {
                    emailRequest.Content = new EmailContent();
                    emailRequest.Content.Subject = this.templateHelper.ReplacePlaceholders(compiledTemplateSubject, personalization.DynamicTemplateData) ?? emailTemplate.Subject;
                    emailRequest.Content.Html = this.templateHelper.ReplacePlaceholders(compiledTemplateBody, personalization.DynamicTemplateData) ?? emailTemplate.Body;
                }

                var validRecipients = FilterInvalidEmails(personalization.EmailRecipients, out invalidEmails);
                if (invalidEmails.Count > 0)
                {
                    logger.LogError("Ignoring invalid recipient email addresses for email request {emailRequestId}: {invalidEmails}", emailRequest.EmailRequestId, string.Join(", ", invalidEmails.Distinct()));
                }

                // Setting content and recipients for email
                RemoveEscapeCharacters(validRecipients);
                emailRequest.Content.Subject = RemoveEscapeCharacters(emailRequest.Content.Subject);
                emailBuilder
                    .SetContent(emailRequest.Content)
                    .SetRecipients(validRecipients);

                var email = emailBuilder.Build();
                if (email.Recipients is null)
                {
                    logger.LogError("Email request with id {id} doesn't have any valid recipients.", emailRequest.EmailRequestId);
                    return;
                }

                var emailId = await emailRepository.SaveEmailAsync(email);

                // Raise event to send email
                var sendEmailRequestEvent = new SendEmailEvent(appContext.TenantId, appContext.UserId, emailId);
                await eventBus.PublishAsync(sendEmailRequestEvent);
            }

            // Delete email request from DB after enriching.
            await emailRequestRepository.DeleteEmailRequestAsync(emailRequestId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error enriching email request with id {id}", emailRequestId);
        }
    }

    [GeneratedRegex(EmailPattern)]
    private static partial Regex EmailRegex();

    private static bool ValidateEmail(string email)
    {
        try
        {
            if (email != null)
            {
                var regex = EmailRegex();
                return regex.IsMatch(email);
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool ValidateEmailRequest(EmailRequestDto emailRequestDto, out ErrorCode? errorCode, out string errorMessage)
    {
        errorCode = null;
        errorMessage = null;
        if (!ValidateEmail(emailRequestDto.From.Address))
        {
            errorCode = ErrorCode.INVALID_EMAIL;
            errorMessage = "Request has invalid From email address.";
            return false;
        }

        return true;
    }

    private static List<Core.Models.EmailMessages.EmailAddress> FilterInvalidEmails(List<Core.Models.EmailMessages.EmailAddress> emailAddresses, ref List<string> invalidEmails)
    {
        invalidEmails = invalidEmails ?? new ();
        emailAddresses = emailAddresses ?? new ();
        var validEmailAddresses = new List<Core.Models.EmailMessages.EmailAddress>();
        foreach (var emailAddress in emailAddresses)
        {
            if (ValidateEmail(emailAddress.Address))
            {
                validEmailAddresses.Add(emailAddress);
            }
            else
            {
                invalidEmails.Add(emailAddress.Address);
            }
        }

        return validEmailAddresses;
    }

    private static EmailRecipients FilterInvalidEmails(EmailRecipients emailRecipients, out List<string> invalidEmails)
    {
        invalidEmails = new ();
        var validTo = FilterInvalidEmails(emailRecipients.To.ToList(), ref invalidEmails);
        var validCc = FilterInvalidEmails(emailRecipients.Cc.ToList(), ref invalidEmails);
        var validBcc = FilterInvalidEmails(emailRecipients.Bcc.ToList(), ref invalidEmails);

        return new EmailRecipients(validTo, validCc, validBcc);
    }

    [GeneratedRegex(EscapeCharactersPattern)]
    private static partial Regex EscapeCharactersPatternRegex();

    [GeneratedRegex(MultipleWhiteSpacePattern)]
    private static partial Regex MultipleWhiteSpacePatternRegex();

    private static string RemoveEscapeCharacters(string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            input = EscapeCharactersPatternRegex().Replace(input, " ");
            return MultipleWhiteSpacePatternRegex().Replace(input, " ");
        }

        return input;
    }

    private static void RemoveEscapeCharacters(Core.Models.EmailMessages.EmailAddress emailAddress)
    {
        emailAddress.DisplayName = RemoveEscapeCharacters(emailAddress.DisplayName);
    }

    private static void RemoveEscapeCharacters(IEnumerable<Core.Models.EmailMessages.EmailAddress> emailAddresses)
    {
        foreach (var item in emailAddresses)
        {
            RemoveEscapeCharacters(item);
        }
    }

    private static void RemoveEscapeCharacters(EmailRecipients emailRecipients)
    {
        RemoveEscapeCharacters(emailRecipients.To);
        RemoveEscapeCharacters(emailRecipients.Cc);
        RemoveEscapeCharacters(emailRecipients.Bcc);
    }

    private async Task<List<EmailAttachment>> SyncEmailAttachments(EmailRequestDto emailRequestDto)
    {
        var attachments = new List<EmailAttachment>();

        if (emailRequestDto.Attachments is null)
        {
            return attachments;
        }

        foreach (var attachment in emailRequestDto.Attachments)
        {
            using (var stream = new MemoryStream(attachment.Content))
            {
               var filePath = await emailAttachmentRepository.SaveEmailAttachmentAsync(stream, attachment.ContentType);

               attachments.Add(new EmailAttachment()
               {
                   Name = attachment.Name,
                   FilePath = filePath,
                   ContentType = attachment.ContentType,
               });
            }
        }

        return attachments;
    }
}