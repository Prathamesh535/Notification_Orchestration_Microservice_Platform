// -----------------------------------------------------------------------
// <copyright file="SendPushNotificationEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents new push notification send event.
/// </summary>
public record SendPushNotificationEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendPushNotificationEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="pushNotificationRequestId">The push notification request id.</param>
    /// <param name="partitionKey">The partition key.</param>
    public SendPushNotificationEvent(Guid tenantId, Guid userId, Guid pushNotificationRequestId, string partitionKey)
        : base(tenantId, userId)
    {
        this.PushNotificationRequestId = pushNotificationRequestId;
        this.PartitionKey = partitionKey;
    }

    /// <summary>
    /// Gets the push notification request identifier.
    /// </summary>
    public Guid PushNotificationRequestId { get; }

    /// <summary>
    /// Gets the partitionKey.
    /// </summary>
    public string PartitionKey { get; }
}
