// -----------------------------------------------------------------------
// <copyright file="InAppNotificationRequestReceivedEventHandler.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker.EventHandlers;

/// <summary>
/// Represents the in app notification request received event handler.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="inAppNotificationService">The in app notification service.</param>
public class InAppNotificationRequestReceivedEventHandler(ILogger<InAppNotificationRequestReceivedEventHandler> logger, IInAppNotificationService inAppNotificationService)
    : IEventHandler<InAppNotificationRequestReceivedEvent>
{
    private readonly ILogger<InAppNotificationRequestReceivedEventHandler> logger = logger;
    private readonly IInAppNotificationService inAppNotificationService = inAppNotificationService;

    /// <inheritdoc/>
    public async Task Handle(InAppNotificationRequestReceivedEvent @event)
    {
        try
        {
            await this.inAppNotificationService.EnrichInAppNotificationRequestAsync(@event.InAppNotificationRequestId, @event.PartitionKey);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured while handling In App Notification Request Received Event with request Id {requestId}. {message}", @event.InAppNotificationRequestId, ex.Message);
        }
    }
}