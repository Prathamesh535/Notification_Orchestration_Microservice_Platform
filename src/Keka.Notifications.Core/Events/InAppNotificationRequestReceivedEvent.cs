// -----------------------------------------------------------------------
// <copyright file="InAppNotificationRequestReceivedEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents in app notification request received event.
/// </summary>
public record InAppNotificationRequestReceivedEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InAppNotificationRequestReceivedEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="inAppNotificationRequestId">The in application notification request identifier.</param>
    /// <param name="partitionKey">InApplicationRequest Partition Key.</param>
    public InAppNotificationRequestReceivedEvent(Guid tenantId, Guid userId, string inAppNotificationRequestId, string partitionKey)
        : base(tenantId, userId)
    {
        this.InAppNotificationRequestId = inAppNotificationRequestId;
        this.PartitionKey = partitionKey;
    }

    /// <summary>
    /// Gets the in app notification request identifier.
    /// </summary>
    public string InAppNotificationRequestId { get; }

    /// <summary>
    /// Gets the partitionKey.
    /// </summary>
    public string PartitionKey { get; }
}