// -----------------------------------------------------------------------
// <copyright file="IEmployeeNotificationPreferenceRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the employee notification preference repository.
/// </summary>
public interface IEmployeeNotificationPreferenceRepository
{
    /// <summary>
    /// Inserts Employee Notification Preferences.
    /// </summary>
    /// <param name="employeeNotificationPreferences"> The Employee Notification Preference List. </param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation that returns the number of rows inserted.</returns>
    Task<int> InsertEmployeeNotificationPreferencesAsync(List<EmployeeNotificationPreference> employeeNotificationPreferences);

    /// <summary>
    /// Gets the disabled employee notification preferences.
    /// </summary>
    /// <param name="employeeId"> The Employee Id. </param>
    /// <returns>A list of existing employee notification preferences.</returns>
    Task<List<EmployeeNotificationPreference>> GetDisabledEmployeeNotificationPreferencesAsync(Guid employeeId);

    /// <summary>
    /// Gets the employee notification preferences.
    /// </summary>
    /// <param name="employeeId"> The Employee Id. </param>
    /// <returns>A list of employee notification preferences.</returns>
    Task<List<EmployeeNotificationPreference>> GetEmployeeNotificationPreferencesAsync(Guid employeeId);

    /// <summary>
    /// Deletes Employee Notification Preferences.
    /// </summary>
    /// <param name="employeeNotificationPreferences"> The Employee Notification Preference List. </param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation that returns the number of rows deleted.</returns>
    Task<int> DeleteEmployeeNotificationPreferencesAsync(List<EmployeeNotificationPreference> employeeNotificationPreferences);
}