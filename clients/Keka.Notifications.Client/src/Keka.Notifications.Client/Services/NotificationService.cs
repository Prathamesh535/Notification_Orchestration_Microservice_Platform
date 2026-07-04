using Keka.HTTP;
using Keka.MultiTenancy;
using Keka.Notifications.Client.Models;
using Keka.Notifications.Client.Models.EmailTemplates;
using Keka.Notifications.Client.Models.Webhook;

namespace Keka.Notifications.Client.Services;

internal partial class NotificationService : INotificationService
{
    private readonly string baseUrl;
    private readonly IHttpClient httpClient;

    public NotificationService(IHttpClient httpClient, HttpClientOptions options, IAppContext appContext)
    {
        this.httpClient = httpClient;
        this.baseUrl = $"{options.Services["notifications"]}/api/v1";
        this.httpClient.SetHeaders((o) =>
        {
            o.Add("X-Tenant-Id", appContext.TenantId.ToString());
            if (appContext.AppContextType == AppContextType.User)
            {
                o.Add("X-User-Id", appContext.UserId.ToString());    
            }
        });
    }
    
    public async Task<Guid?> SendSmsAsync(SmsRequest smsRequest)
    {
        var response = await httpClient.PostAsync<Response<Guid?>>(Api.MessagingNotifications.SendSms(this.baseUrl), smsRequest);
        return response?.Data;
    }

    public async Task<Guid?> SendWhatsAppMessageAsync(WaMessageRequest waMessageRequest)
    {
        var response = await httpClient.PostAsync<Response<Guid?>>(Api.MessagingNotifications.SendWhatsAppMessage(this.baseUrl), waMessageRequest);
        return response?.Data;
    }

    public async Task<Guid?> SendPushNotificationAsync(PushNotificationRequest pushNotificationRequest)
    {
        var response = await httpClient.PostAsync<Response<Guid?>>(Api.MessagingNotifications.SendPushNotification(this.baseUrl), pushNotificationRequest);
        return response?.Data;
    }

#nullable enable
    public async Task<string?> SendInAppNotificationAsync(InAppNotificationRequest inAppNotificationRequest)
    {
        var response = await httpClient.PostAsync<Response<string?>>(Api.MessagingNotifications.SendInAppNotification(this.baseUrl), inAppNotificationRequest);
        return response?.Data;
    }
#nullable restore

    public async Task<Guid?> SendWebhookRequestAsync(WebhookRequest webhookRequest)
    {
        var response = await httpClient.PostAsync<Response<Guid?>>(Api.MessagingNotifications.SendWebhookRequest(this.baseUrl), webhookRequest);
        return response?.Data;
    }

    public async Task<Guid?> SendSlackNotificationRequestAsync(SlackNotificationRequest slackNotificationRequest)
    {
        var response = await httpClient.PostAsync<Response<Guid?>>(Api.MessagingNotifications.SendSlackNotification(this.baseUrl), slackNotificationRequest);
        return response?.Data;
    }
}
