// -----------------------------------------------------------------------
// <copyright file="EmailRequestReceivedEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents the email request received integration event.
/// </summary>
public record EmailRequestReceivedEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRequestReceivedEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="emailRequestId">The email request id.</param>
    public EmailRequestReceivedEvent(Guid tenantId, Guid userId, string emailRequestId)
        : base(tenantId, userId)
    {
        this.EmailRequestId = emailRequestId;
    }

    /// <summary>
    /// Gets the email request identifier.
    /// </summary>
    public string EmailRequestId { get; }
}