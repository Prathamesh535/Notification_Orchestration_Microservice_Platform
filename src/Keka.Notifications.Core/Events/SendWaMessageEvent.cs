// -----------------------------------------------------------------------
// <copyright file="SendWaMessageEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents new whatsapp message send event.
/// </summary>
public record SendWaMessageEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendWaMessageEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="waMessageId">The whatsapp message id.</param>
    public SendWaMessageEvent(Guid tenantId, Guid userId, string waMessageId)
        : base(tenantId, userId)
    {
        this.WaMessageId = waMessageId;
    }

    /// <summary>
    /// Gets the email identifier.
    /// </summary>
    public string WaMessageId { get; }
}
