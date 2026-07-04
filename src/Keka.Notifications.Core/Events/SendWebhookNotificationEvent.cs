// -----------------------------------------------------------------------
// <copyright file="SendWebhookNotificationEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents an event for sending a webhook notification.
/// </summary>
public record class SendWebhookNotificationEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendWebhookNotificationEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The unique identifier for the tenant associated with the webhook notification.</param>
    /// <param name="userId">The unique identifier for the user initiating the webhook notification.</param>
    /// <param name="webhookRequestId">The unique identifier for the webhook request.</param>
    /// <param name="partitionKey">Webhook request Partition Key.</param>
    public SendWebhookNotificationEvent(Guid tenantId, Guid userId, Guid webhookRequestId, string partitionKey)
        : base(tenantId, userId)
    {
        this.WebhookRequestId = webhookRequestId;
        this.PartitionKey = partitionKey;
    }

    /// <summary>
    /// Gets the unique identifier for the webhook request.
    /// </summary>
    public Guid WebhookRequestId { get; }

    /// <summary>
    /// Gets the partitionKey.
    /// </summary>
    public string PartitionKey { get; }
}
