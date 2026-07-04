// -----------------------------------------------------------------------
// <copyright file="EmployeeAddedEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents the employee added event.
/// </summary>
public record EmployeeAddedEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeAddedEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="employeeId">The employeeId identifier.</param>
    /// <param name="email">The employeeId email.</param>
    public EmployeeAddedEvent(Guid tenantId, Guid userId, Guid employeeId, string email)
        : base(tenantId, userId)
    {
        this.EmployeeId = employeeId;
        this.Email = email;
    }

    /// <summary>
    /// Gets the employee identifier.
    /// </summary>
    public Guid EmployeeId { get; }

    /// <summary>
    /// Gets the employee email.
    /// </summary>
    public string Email { get; }
}