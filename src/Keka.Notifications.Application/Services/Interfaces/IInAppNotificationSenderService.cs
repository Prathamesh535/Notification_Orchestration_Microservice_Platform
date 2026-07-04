// -----------------------------------------------------------------------
// <copyright file="IInAppNotificationSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents an in app notification sender service.
/// </summary>
public interface IInAppNotificationSenderService
{
    /// <summary>
    /// Sends the web push notification asynchronously.
    /// </summary>
    /// <param name="sendInAppNotificationEvent">The send inapp notification event.</param>
    /// <returns>Returns task.</returns>
    Task SendInAppNotificationWebPushAsync(SendInAppNotificationEvent sendInAppNotificationEvent);
}