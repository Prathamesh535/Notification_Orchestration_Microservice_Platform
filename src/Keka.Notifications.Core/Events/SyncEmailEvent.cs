// -----------------------------------------------------------------------
// <copyright file="SyncEmailEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents the sync email event.
/// </summary>
public record SyncEmailEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SyncEmailEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="emailId">The email identifier.</param>
    /// <param name="externalId">The external identifier.</param>
    public SyncEmailEvent(Guid tenantId, Guid userId, string emailId, string externalId)
        : base(tenantId, userId)
    {
        this.EmailId = emailId;
        this.ExternalId = externalId;
    }

    /// <summary>
    /// Gets the email identifier.
    /// </summary>
    public string EmailId { get; }

    /// <summary>
    /// Gets the external identifier.
    /// </summary>
    public string ExternalId { get; }
}