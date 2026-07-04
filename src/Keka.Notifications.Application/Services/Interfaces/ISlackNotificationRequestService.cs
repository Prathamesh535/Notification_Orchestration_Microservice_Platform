// -----------------------------------------------------------------------
// <copyright file="ISlackNotificationRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents slack notification service.
/// </summary>
public interface ISlackNotificationRequestService
{
    /// <summary>
    /// Method to add slack notification to db.
    /// </summary>
    /// <param name="slackNotificationRequestDto">Slack notification request dto.</param>
    /// <returns>Slack notification request Id.</returns>
    Task<Guid> AddSlackNotificationRequestAsync(SlackNotificationRequestDto slackNotificationRequestDto);
}
