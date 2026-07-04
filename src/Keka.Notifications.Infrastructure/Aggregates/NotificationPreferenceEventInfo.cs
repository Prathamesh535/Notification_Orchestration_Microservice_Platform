// -----------------------------------------------------------------------
// <copyright file="NotificationPreferenceEventInfo.cs" company="Keka Inc">
// Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Infrastructure.Aggregates;

/// <summary>
/// Represents notification preference event info.
/// </summary>
internal class NotificationPreferenceEventInfo
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
    public string NotificationChannels { get; set; }

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
