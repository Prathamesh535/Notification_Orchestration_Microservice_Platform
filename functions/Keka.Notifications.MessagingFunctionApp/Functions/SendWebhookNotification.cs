// -----------------------------------------------------------------------
// <copyright file="SendWebhookNotification.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.MessagingFunctionApp.Functions;

/// <summary>
/// Handles sending webhook notifications based on messages from the Service Bus.
/// Initializes a new instance of the <see cref="SendWebhookNotification"/> class.
/// </summary>
/// <param name="rootScope">The root scope used for dependency resolution.</param>
/// <param name="logger">The logger service for logging messages and errors.</param>
/// <param name="webhookNotificationSenderService">The service responsible for sending webhook notifications.</param>
public class SendWebhookNotification(ILifetimeScope rootScope, ILogger<SendWebhookNotification> logger,
                                 IWebhookNotificationSenderService webhookNotificationSenderService)
{
    [Function(nameof(SendWebhookNotification))]
    public async Task RunAsync([ServiceBusTrigger("keka_event_bus", "keka_notifications_webhook", Connection = "EventBus")] string message)
    {
        if (!message.TryDeserialize(out SendWebhookNotificationEvent sendWaMessageEvent))
        {
            logger.LogError("Invalid message: {Message}", message);
            return;
        }
        try
        {
            rootScope.Resolve<IAppContext>().TenantId = sendWaMessageEvent.TenantId;
            rootScope.Resolve<IAppContext>().UserId = sendWaMessageEvent.UserId;

            await webhookNotificationSenderService.SendWebhookNotificationAsync(sendWaMessageEvent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending webhook notification for request id: {RequestId}", sendWaMessageEvent.WebhookRequestId);
        }
    }
}