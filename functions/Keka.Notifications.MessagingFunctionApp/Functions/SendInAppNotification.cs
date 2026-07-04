// -----------------------------------------------------------------------
// <copyright file="SendInAppNotification.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.MessagingFunctionApp.Functions;

/// <summary>
/// Handles the sending of in-app notification via a service bus trigger.
/// </summary>
public class SendInAppNotification
{
    private readonly ILifetimeScope rootScope;
    private readonly ILogger<SendInAppNotification> logger;
    private readonly IInAppNotificationSenderService inAppNotificationSenderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendInAppNotification"/> class.
    /// </summary>
    /// <param name="rootScope">The root scope.</param>
    /// <param name="logger">The logger service.</param>
    /// <param name="inAppNotificationSenderService">The in-app notification sender service.</param>
    public SendInAppNotification(ILifetimeScope rootScope, ILogger<SendInAppNotification> logger, IInAppNotificationSenderService inAppNotificationSenderService)
    {
        this.rootScope = rootScope;
        this.logger = logger;
        this.inAppNotificationSenderService = inAppNotificationSenderService;
    }

    /// <summary>
    /// Processes in-app notifications from the service bus and sends in-app web push notification.
    /// </summary>
    /// <param name="mySbMsg">The message from the service bus.</param>
    [Function(nameof(SendInAppNotification))]
    public async Task Run([ServiceBusTrigger("keka_event_bus", "keka_notifications_inapp", Connection = "EventBus")] string mySbMsg)
    {
        SendInAppNotificationEvent sendInAppNotificationEvent;
        if (!mySbMsg.TryDeserialize(out sendInAppNotificationEvent))
        {
            logger.LogError("Invalid message: {message}", mySbMsg);
            return;
        }

        try
        {
            rootScope.Resolve<IAppContext>().TenantId = sendInAppNotificationEvent.TenantId;
            rootScope.Resolve<IAppContext>().UserId = sendInAppNotificationEvent.UserId;
            await inAppNotificationSenderService.SendInAppNotificationWebPushAsync(sendInAppNotificationEvent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while sending in-app web push notification for request id: {inAppNotificationId}", sendInAppNotificationEvent.InAppNotificationId);
        }
    }
}