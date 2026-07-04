// -----------------------------------------------------------------------
// <copyright file="EmployeeNotificationPreference.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Employees;

/// <summary>
/// Represents the notification preferences for an employee.
/// </summary>
public class EmployeeNotificationPreference
{
    /// <summary>
    /// Gets or sets the employee id.
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the event code.
    /// </summary>
    public string EventCode { get; set; }

    /// <summary>
    /// Gets or sets the event code.
    /// </summary>
    public List<NotificationChannel> NotificationChannels { get; set; }

    /// <summary>
    /// Gets or sets the module name.
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// Gets or sets the event name.
    /// </summary>
    public string EventName { get; set; }

#nullable enable
    /// <summary>
    /// Gets or sets the event description.
    /// </summary>
    public string? EventDescription { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets a value indicating whether the notification is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }
}