// -----------------------------------------------------------------------
// <copyright file="SaveEmployeeNotificationPreferenceDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Employee;

/// <summary>
/// Represents the employee notification preference dto.
/// </summary>
public class SaveEmployeeNotificationPreferenceDto
{
    /// <summary>
    /// Gets or sets the event id.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether IsEnabled is true or false.
    /// </summary>
    public bool IsEnabled { get; set; }
}