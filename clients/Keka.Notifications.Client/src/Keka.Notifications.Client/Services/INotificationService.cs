namespace Keka.Notifications.Client.Services;

public partial interface INotificationService
{
    Task<Guid?> SendSmsAsync(SmsRequest smsRequest);

    Task<Guid?> SendWhatsAppMessageAsync(WaMessageRequest waMessageRequest);

    Task<Guid?> SendPushNotificationAsync(PushNotificationRequest pushNotificationRequest);

#nullable enable
    Task<string?> SendInAppNotificationAsync(InAppNotificationRequest inAppNotificationRequest);
#nullable restore

    Task<Guid?> SendWebhookRequestAsync(WebhookRequest webhookRequest);

    Task<Guid?> SendSlackNotificationRequestAsync(SlackNotificationRequest slackNotificationRequest);
}
