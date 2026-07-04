// -----------------------------------------------------------------------
// <copyright file="IEmployeeRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// represents the employee repository.
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// Gets the employee by identifier asynchronous.
    /// </summary>
    /// <param name="employeeId">The employee identifier.</param>
    /// <returns>returns the employee.</returns>
    Task<Employee> GetEmployeeByIdAsync(Guid employeeId);

    /// <summary>
    /// Adds the employee asynchronous.
    /// </summary>
    /// <param name="employee">The employee.</param>
    /// <returns>The task result.</returns>
    Task AddEmployeeAsync(Employee employee);
}
