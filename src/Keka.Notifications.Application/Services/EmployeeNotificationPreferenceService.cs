// -----------------------------------------------------------------------
// <copyright file="EmployeeNotificationPreferenceService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents employee notification preference service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EmployeeNotificationPreferenceService"/> class.
/// </remarks>
/// <param name="logger">The logger instance.</param>
/// <param name="mapper">The mapper instance.</param>
/// <param name="unitOfWork">The unit of work instance.</param>
/// <param name="appContext">The application context containing information about the current user and environment.</param>
public class EmployeeNotificationPreferenceService(ILogger<EmployeeNotificationPreferenceService> logger, IMapper mapper, IUnitOfWork unitOfWork, IAppContext appContext)
    : BaseService(logger, mapper, unitOfWork), IEmployeeNotificationPreferenceService
{
    /// <inheritdoc />
    public async Task<bool> SaveEmployeeNotificationPreferencesAsync(List<SaveEmployeeNotificationPreferenceDto> employeeNotificationPreferenceDtos)
    {
        var employeeNotificationPreferences = this.Mapper.Map<List<EmployeeNotificationPreference>>(employeeNotificationPreferenceDtos);

        // Set the EmployeeId for each preference to the current user's ID.
        employeeNotificationPreferences.ForEach(pref =>
        {
            pref.EmployeeId = appContext.UserId;
        });

        // Filter out only the disabled preferences from the input list.
        var disabledPreferences = employeeNotificationPreferences.Where(dto => !dto.IsEnabled).ToList();

        // Retrieve existing disabled preferences for the current user from the repository.
        var existingDisabledPreferences = await this.UnitOfWork.EmployeeNotificationPreferenceRepository.GetDisabledEmployeeNotificationPreferencesAsync(appContext.UserId);

        // Determine which preferences need to be inserted (newly disabled preferences).
        var preferencesToInsert = disabledPreferences
            .Where(pref => !existingDisabledPreferences.Exists(ep => ep.EventId == pref.EventId))
            .ToList();

        // Determine which preferences need to be deleted (already existing disabled preferences).
        var preferencesToDelete = existingDisabledPreferences
            .Where(edp => !employeeNotificationPreferences.Exists(enp => enp.EventId == edp.EventId))
            .Union(employeeNotificationPreferences.Where(pref => existingDisabledPreferences.Exists(ep => ep.EventId == pref.EventId && pref.IsEnabled)))
            .ToList();

        var totalCount = 0;

        // Insert new disabled preferences if any.
        if (preferencesToInsert.Count > 0)
        {
            totalCount += await this.UnitOfWork.EmployeeNotificationPreferenceRepository.InsertEmployeeNotificationPreferencesAsync(preferencesToInsert);
        }

        // Delete existing disabled preferences if any.
        if (preferencesToDelete.Count > 0)
        {
            totalCount += await this.UnitOfWork.EmployeeNotificationPreferenceRepository.DeleteEmployeeNotificationPreferencesAsync(preferencesToDelete);
        }

        return totalCount > 0;
    }

    /// <inheritdoc/>
    public async Task<List<EmployeeNotificationPreferenceDto>> GetEmployeeNotificationPreferencesAsync()
    {
        var employeeNotificationPreferences = await this.UnitOfWork.EmployeeNotificationPreferenceRepository.GetEmployeeNotificationPreferencesAsync(appContext.UserId);
        return this.Mapper.Map<List<EmployeeNotificationPreferenceDto>>(employeeNotificationPreferences);
    }
}