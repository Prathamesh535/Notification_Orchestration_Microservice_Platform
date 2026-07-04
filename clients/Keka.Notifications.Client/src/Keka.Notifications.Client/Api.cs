namespace Keka.Notifications.Client;

internal static class Api
{
    internal static class EmailNotifications
    {
        public static string CreateEmailTemplate(string baseUrl) => $"{baseUrl}/emails/templates";

        public static string UpdateEmailTemplate(string baseUrl, Guid templateId) => $"{baseUrl}/emails/templates/{templateId}";

        public static string GetEmailTemplate(string baseUrl, Guid templateId) => $"{baseUrl}/emails/templates/{templateId}";

        public static string SendEmail(string baseUrl) => $"{baseUrl}/emails/send";
        
        public static string GetEmailStatus(string baseUrl)  => $"{baseUrl}/emails/status";
        
        public static string GetBlockedEmailsInPastDay(string baseUrl)  => $"{baseUrl}/emails/blocked";
    }

    internal static class MessagingNotifications
    {
        public static string SendSms(string baseUrl) => $"{baseUrl}/sms/send";
        public static string SendWhatsAppMessage(string baseUrl) => $"{baseUrl}/whatsapp/send";
        public static string SendPushNotification(string baseUrl) => $"{baseUrl}/push-notifications/send";
        public static string SendInAppNotification(string baseUrl) => $"{baseUrl}/in-app-notifications/send";
        public static string SendSlackNotification(string baseUrl) => $"{baseUrl}/slack/send";
        public static string SendWebhookRequest(string baseUrl) => $"{baseUrl}/webhooks/send";
    }
}
