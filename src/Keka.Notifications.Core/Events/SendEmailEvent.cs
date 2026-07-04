// -----------------------------------------------------------------------
// <copyright file="SendEmailEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents the send email request integration event.
/// </summary>
public record SendEmailEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendEmailEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="emailId">The email id.</param>
    public SendEmailEvent(Guid tenantId, Guid userId, string emailId)
        : base(tenantId, userId)
    {
        this.EmailId = emailId;
    }

    /// <summary>
    /// Gets the email identifier.
    /// </summary>
    public string EmailId { get; }
}