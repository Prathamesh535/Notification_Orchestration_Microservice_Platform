// -----------------------------------------------------------------------
// <copyright file="NotificationPreferencesController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.V1;

/// <summary>
/// Represents the rest endpoints for notification preferences.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/notification-preferences")]
public class NotificationPreferencesController : BaseApiController
{
    private readonly IEmployeeNotificationPreferenceService employeeNotificationPreferenceService;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationPreferencesController"/> class.
    /// </summary>
    /// <param name="employeeNotificationPreferenceService">The employee notification preference service.</param>
    public NotificationPreferencesController(IEmployeeNotificationPreferenceService employeeNotificationPreferenceService)
    {
        this.employeeNotificationPreferenceService = employeeNotificationPreferenceService;
    }

    /// <summary>
    /// Saves employee notification preferences.
    /// </summary>
    /// <param name="employeeNotificationPreferenceDtos">A list of employee notification preferences.</param>
    /// <returns>Returns a boolean that indicates success or not.</returns>
    [HttpPost]
    public async Task<IActionResult> SaveEmployeeNotificationPreferences([FromBody] List<SaveEmployeeNotificationPreferenceDto> employeeNotificationPreferenceDtos)
    {
        var isSuccess = await this.employeeNotificationPreferenceService.SaveEmployeeNotificationPreferencesAsync(employeeNotificationPreferenceDtos);
        return this.ToOkResponse(isSuccess);
    }

    /// <summary>
    /// Gets the employee notification preferences.
    /// </summary>
    /// <returns>A list of employee notification preferences.</returns>
    [HttpGet]
    public async Task<IActionResult> GetEmployeeNotificationPreferencesAsync()
    {
        var employeeNotificationPreferenceDtos = await this.employeeNotificationPreferenceService.GetEmployeeNotificationPreferencesAsync();
        return this.ToOkResponse(employeeNotificationPreferenceDtos);
    }
}