// -----------------------------------------------------------------------
// <copyright file="IEmployeeNotificationPreferenceService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents the interface for employee notification preference service.
/// </summary>
public interface IEmployeeNotificationPreferenceService
{
    /// <summary>
    /// Gets the employee notification preferences asynchronously.
    /// </summary>
    /// <returns>A List of employee notification preference dtos.</returns>
    Task<List<EmployeeNotificationPreferenceDto>> GetEmployeeNotificationPreferencesAsync();

    /// <summary>
    /// Inserts or deletes an employee's email notification preferences.
    /// Only disabled notification preferences are saved. This is because the retrieval of notification preferences uses a LEFT JOIN, which returns all possible events for an employee with a default value of IsEnabled as true.
    /// This method ensures that only explicitly disabled preferences are stored in the database.
    /// </summary>
    /// <param name="employeeNotificationPreferenceDtos">The employee's notification preferences details to insert or delete.</param>
    /// <returns>A task with a boolean indicating success or failure.</returns>
    Task<bool> SaveEmployeeNotificationPreferencesAsync(List<SaveEmployeeNotificationPreferenceDto> employeeNotificationPreferenceDtos);
}