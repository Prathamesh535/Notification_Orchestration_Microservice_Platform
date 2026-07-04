using Keka.Notifications.Client.Models;
using Keka.Notifications.Client.Models.EmailTemplates;

namespace Keka.Notifications.Client.Services;

internal partial class NotificationService : INotificationService
{
    public async Task<Guid?> CreateEmailTemplateAsync(EmailTemplateCreateRequest emailTemplateCreateRequest)
    {
        var apiUrl = Api.EmailNotifications.CreateEmailTemplate(this.baseUrl);
        var response = await httpClient.PostAsync<Response<Guid?>>(apiUrl, emailTemplateCreateRequest);

        return response?.Data;
    }

    public async Task<bool?> UpdateEmailTemplateAsync(Guid emailTemplateId, EmailTemplateUpdateRequest emailTemplateUpdateRequest)
    {
        var apiUrl = Api.EmailNotifications.UpdateEmailTemplate(this.baseUrl, emailTemplateId);
        var response = await httpClient.PutAsync<Response<bool?>>(apiUrl, emailTemplateUpdateRequest);

        return response?.Data;
    }

#nullable enable
    public async Task<EmailTemplate?> GetEmailTemplateAsync(Guid emailTemplateId)
    {
        var apiUrl = Api.EmailNotifications.GetEmailTemplate(this.baseUrl, emailTemplateId);
        var response = await httpClient.GetAsync<Response<EmailTemplate?>>(apiUrl);

        return response?.Data;
    }
#nullable restore

    public async Task<Guid?> SendEmailAsync(EmailRequest email)
    {
        var response = await httpClient.PostAsync<Response<Guid?>>(Api.EmailNotifications.SendEmail(this.baseUrl), email);
        return response?.Data;
    }

    public async Task<List<EmailStatusDto>> GetEmailStatus(EmailStatusRequestDto emailStatusRequest)
    {
        var response = await httpClient.PostAsync<Response<List<EmailStatusDto>>>(Api.EmailNotifications.GetEmailStatus(this.baseUrl), emailStatusRequest);
        return response?.Data;
    }

    public async Task<List<EmailStatusDto>> GetBlockedEmailsInPastDay()
    {
        var response = await httpClient.PostAsync<Response<List<EmailStatusDto>>>(Api.EmailNotifications.GetBlockedEmailsInPastDay(this.baseUrl));
        return response?.Data;
    }
}
