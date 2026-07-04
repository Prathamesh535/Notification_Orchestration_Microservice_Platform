// -----------------------------------------------------------------------
// <copyright file="ISlackRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the slack notification request repository.
/// </summary>
public interface ISlackRequestRepository
{
    /// <summary>
    /// Saves slack notification requests to db.
    /// </summary>
    /// <param name="slackRequest">Slack notification request.</param>
    /// <returns>Slack notification request Id and partition key.</returns>
    Task<(Guid requestId, string partitionKey)> SaveSlackNotificationRequestAsync(SlackNotificationRequest slackRequest);

    /// <summary>
    /// Gets slack notification request data based on slack request id.
    /// </summary>
    /// <param name="slackRequestId">The slack Notification Request.</param>
    /// <param name="partitionKey">The slack notification request partitionkey.</param>
    /// <returns>SlackNotification.</returns>
    Task<SlackNotificationRequest> GetSlackNotificationRequestAsync(Guid slackRequestId, string partitionKey);

    /// <summary>
    /// Updates salck notification request status, raw response and hasException.
    /// </summary>
    /// <param name="slackNotificationRequest">Instance of slack notification request.</param>
    /// <param name="partitionKey">The slack notification request partitionkey.</param>
    /// <returns>Returns task.</returns>
    Task UpdateSlackNotificationAsync(SlackNotificationRequest slackNotificationRequest, string partitionKey);
}
