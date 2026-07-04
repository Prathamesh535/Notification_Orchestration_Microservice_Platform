// -----------------------------------------------------------------------
// <copyright file="EmployeeEmailUpdatedEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents the employee email updated event.
/// </summary>
public record EmployeeEmailUpdatedEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeEmailUpdatedEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="employeeId">The employeeId identifier.</param>
    /// <param name="oldEmail">The old employee email.</param>
    /// <param name="newEmail">The updated employee email.</param>
    public EmployeeEmailUpdatedEvent(Guid tenantId, Guid userId, Guid employeeId, string oldEmail, string newEmail)
        : base(tenantId, userId)
    {
        this.EmployeeId = employeeId;
        this.OldEmail = oldEmail;
        this.NewEmail = newEmail;
    }

    /// <summary>
    /// Gets the employee identifier.
    /// </summary>
    public Guid EmployeeId { get; }

    /// <summary>
    /// Gets the old employee email.
    /// </summary>
    public string OldEmail { get; }

    /// <summary>
    /// Gets the updated employee email.
    /// </summary>
    public string NewEmail { get; }
}