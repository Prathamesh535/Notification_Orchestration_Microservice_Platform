// -----------------------------------------------------------------------
// <copyright file="SendSlackNotification.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.MessageFunctionApp.Functions;

public class SendSlackNotification
{
    private readonly ILifetimeScope rootScope;
    private readonly ILogger<SendSlackNotification> logger;
    private readonly ISlackNotificationSenderService slackNotificationSenderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendSlackNotification"/> class.
    /// </summary>
    /// <param name="rootScope">The root scope.</param>
    /// <param name="logger">The logger service.</param>
    /// <param name="slackNotificationSenderService">The slack notification sender service.</param>
    public SendSlackNotification(ILifetimeScope rootScope, ILogger<SendSlackNotification> logger, ISlackNotificationSenderService slackNotificationSenderService)
    {
        this.rootScope = rootScope;
        this.logger = logger;
        this.slackNotificationSenderService = slackNotificationSenderService;
    }

    /// <summary>
    /// Processes push notifications from the service bus and sends push notification.
    /// </summary>
    /// <param name="mySbMsg">The message from the service bus.</param>
    [Function(nameof(SendSlackNotification))]
    public async Task RunAsync([ServiceBusTrigger("keka_event_bus", "keka_notifications_slack", Connection = "EventBus")] string mySbMsg)
    {
        SendSlackNotificationEvent sendSlackEvent;
        if (!mySbMsg.TryDeserialize(out sendSlackEvent))
        {
            logger.LogError("Invalid message: {message}", mySbMsg);
            return;
        }

        try
        {
            rootScope.Resolve<IAppContext>().TenantId = sendSlackEvent.TenantId;
            rootScope.Resolve<IAppContext>().UserId = sendSlackEvent.UserId;
            await slackNotificationSenderService.SendSlackNotificationAsync(sendSlackEvent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending Slack notification message for request id: {slackNotificationRequestId}", sendSlackEvent.SlackRequestId);
        }
    }
}
