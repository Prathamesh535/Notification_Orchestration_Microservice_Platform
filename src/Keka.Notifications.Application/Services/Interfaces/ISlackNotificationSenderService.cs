// -----------------------------------------------------------------------
// <copyright file="ISlackNotificationSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents slack notification sender service.
/// </summary>
public interface ISlackNotificationSenderService
{
    /// <summary>
    /// Method to send slack notifiaction to specified channel.
    /// </summary>
    /// <param name="sendSlackNotificationEvent">Instence of sendSlackNotificatioEvent.</param>
    /// <returns>Task.</returns>
    Task SendSlackNotificationAsync(SendSlackNotificationEvent sendSlackNotificationEvent);
}
