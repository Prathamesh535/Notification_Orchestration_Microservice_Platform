// -----------------------------------------------------------------------
// <copyright file="IWebhookNotificationSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// This method is responsible for sending a webhook notification and may involve
/// processing the event, making HTTP requests, and updating the status based on the response.
/// </summary>
public interface IWebhookNotificationSenderService
{
    /// <summary>
    /// Sends a webhook notification based on the specified event.
    /// </summary>
    /// <param name="sendWebhookNotificationEvent"> An instance of <see cref="SendWebhookNotificationEvent"/> containing details of the webhook notification to be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendWebhookNotificationAsync(SendWebhookNotificationEvent sendWebhookNotificationEvent);
}
