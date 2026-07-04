// -----------------------------------------------------------------------
// <copyright file="IPushNotificationSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents interface of push notification sender service.
/// </summary>
public interface IPushNotificationSenderService
{
    /// <summary>
    /// Sends push notification.
    /// </summary>
    /// <param name="sendPushNotificationRequestEvent">Send push notification event.</param>
    /// <returns>The task result.</returns>
    Task SendPushNotificationAsync(SendPushNotificationEvent sendPushNotificationRequestEvent);
}
