// -----------------------------------------------------------------------
// <copyright file="SendSlackNotificationEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Event for sending slack notification.
/// </summary>
public record SendSlackNotificationEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendSlackNotificationEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="slackRequestId">The slack notification request id.</param>
    /// <param name="partitionKey">SlackRequest Partition Key.</param>
    public SendSlackNotificationEvent(Guid tenantId, Guid userId, Guid slackRequestId, string partitionKey)
        : base(tenantId, userId)
    {
        this.SlackRequestId = slackRequestId;
        this.PartitionKey = partitionKey;
    }

    /// <summary>
    /// Gets the push notification request identifier.
    /// </summary>
    public Guid SlackRequestId { get; }

    /// <summary>
    /// Gets the partitionKey.
    /// </summary>
    public string PartitionKey { get; }
}
