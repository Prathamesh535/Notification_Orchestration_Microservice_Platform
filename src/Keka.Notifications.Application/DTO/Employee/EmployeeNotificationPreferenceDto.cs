// -----------------------------------------------------------------------
// <copyright file="EmployeeNotificationPreferenceDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Employee;

/// <summary>
/// Represents the employee notification preference dto.
/// </summary>
public class EmployeeNotificationPreferenceDto
{
    /// <summary>
    /// Gets or sets the event id.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the event code.
    /// </summary>
    public string EventCode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether IsEnabled is true or false.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets the module name.
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// Gets or sets the event name.
    /// </summary>
    public string EventName { get; set; }

    /// <summary>
    /// Gets or sets the event description.
    /// </summary>
    public string EventDescription { get; set; }

    /// <summary>
    /// Gets or sets the notification channel.
    /// </summary>
    public List<string> NotificationChannels { get; set; }
}