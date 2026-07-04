// -----------------------------------------------------------------------
// <copyright file="SendPushNotification.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.MessagingFunctionApp.Functions;

/// <summary>
/// Handles the sending of push notification via a service bus trigger.
/// </summary>
public class SendPushNotification
{
    private readonly ILifetimeScope rootScope;
    private readonly ILogger<SendPushNotification> logger;
    private readonly IPushNotificationSenderService pushNotificationSenderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendPushNotification"/> class.
    /// </summary>
    /// <param name="rootScope">The root scope.</param>
    /// <param name="logger">The logger service.</param>
    /// <param name="pushNotificationSenderService">The push notification sender service.</param>
    public SendPushNotification(ILifetimeScope rootScope, ILogger<SendPushNotification> logger, IPushNotificationSenderService pushNotificationSenderService)
    {
        this.rootScope = rootScope;
        this.logger = logger;
        this.pushNotificationSenderService = pushNotificationSenderService;
    }

    /// <summary>
    /// Processes push notifications from the service bus and sends push notification.
    /// </summary>
    /// <param name="mySbMsg">The message from the service bus.</param>
    [Function(nameof(SendPushNotification))]
    public async Task Run([ServiceBusTrigger("keka_event_bus", "keka_notifications_push", Connection = "EventBus")] string mySbMsg)
    {
        SendPushNotificationEvent sendPushNotificationEvent;
        if (!mySbMsg.TryDeserialize(out sendPushNotificationEvent))
        {
            logger.LogError("Invalid message: {message}", mySbMsg);
            return;
        }

        try
        {
            rootScope.Resolve<IAppContext>().TenantId = sendPushNotificationEvent.TenantId;
            rootScope.Resolve<IAppContext>().UserId = sendPushNotificationEvent.UserId;
            await pushNotificationSenderService.SendPushNotificationAsync(sendPushNotificationEvent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending push notification message for request id: {pushNotificationRequestId}", sendPushNotificationEvent.PushNotificationRequestId);
        }
    }
}