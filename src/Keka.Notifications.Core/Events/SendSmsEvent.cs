// -----------------------------------------------------------------------
// <copyright file="SendSmsEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents new Sms Request event.
/// </summary>
public record SendSmsEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendSmsEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="smsRequestId">The Sms Request Id.</param>
    /// <param name="partitionKey">SMSRequest Partition Key.</param>
    public SendSmsEvent(Guid tenantId, Guid userId, Guid smsRequestId, string partitionKey)
        : base(tenantId, userId)
    {
        this.SmsRequestId = smsRequestId;
        this.PartitionKey = partitionKey;
    }

    /// <summary>
    /// Gets the Sms Request Identifier.
    /// </summary>
    public Guid SmsRequestId { get; }

    /// <summary>
    /// Gets the partitionKey.
    /// </summary>
    public string PartitionKey { get; }
}
