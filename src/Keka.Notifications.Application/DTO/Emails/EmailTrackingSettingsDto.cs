// -----------------------------------------------------------------------
// <copyright file="EmailTrackingSettingsDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Represents the email tracking settings data transfer object.
/// </summary>
public class EmailTrackingSettingsDto
{
    /// <summary>
    /// Gets or sets the click tracking settings.
    /// </summary>
    public ClickTrackingDto ClickTracking { get; set; }

    /// <summary>
    /// Gets or sets the open tracking settings.
    /// </summary>
    public OpenTracking OpenTracking { get; set; }
}
