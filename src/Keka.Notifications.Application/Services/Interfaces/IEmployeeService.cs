// -----------------------------------------------------------------------
// <copyright file="IEmployeeService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// represents the employee service interface.
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Asynchronously adds a new employee.
    /// </summary>
    /// <param name="employee">The employee to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddEmployeeAsync(Employee employee);

    /// <summary>
    /// Asynchronously retrieves an employee by their identifier.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee.</param>
    /// <returns>A task representing the asynchronous operation, containing the employee dto.</returns>
    Task<EmployeeDto> GetEmployeeAsync(Guid employeeId);
}
