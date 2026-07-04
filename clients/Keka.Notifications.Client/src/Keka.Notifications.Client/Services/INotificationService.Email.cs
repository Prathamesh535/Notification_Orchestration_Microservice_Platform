using Keka.Notifications.Client.Models.EmailTemplates;

namespace Keka.Notifications.Client.Services;

public partial interface INotificationService
{
    Task<Guid?> CreateEmailTemplateAsync(EmailTemplateCreateRequest emailTemplateCreateRequest);

    Task<bool?> UpdateEmailTemplateAsync(Guid emailTemplateId, EmailTemplateUpdateRequest emailTemplateUpdateRequest);

#nullable enable
    Task<EmailTemplate?> GetEmailTemplateAsync(Guid emailTemplateId);
#nullable restore

    Task<Guid?> SendEmailAsync(EmailRequest email);

    Task<List<EmailStatusDto>> GetEmailStatus(EmailStatusRequestDto emailStatusRequest);

    Task<List<EmailStatusDto>> GetBlockedEmailsInPastDay();
}