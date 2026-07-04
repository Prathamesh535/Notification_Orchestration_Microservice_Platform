// -----------------------------------------------------------------------
// <copyright file="Employee.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Employees;

/// <summary>
/// Represent the employee entity.
/// </summary>
public class Employee
{
    /// <summary>
    /// Gets or sets the employee id.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the employee unique identifier.
    /// </summary>
    public Guid Identifier { get; set; }
}
