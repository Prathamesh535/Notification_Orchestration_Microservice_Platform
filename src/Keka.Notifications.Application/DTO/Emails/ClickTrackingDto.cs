// -----------------------------------------------------------------------
// <copyright file="ClickTrackingDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Class to represent the email click tracking data transfer object.
/// </summary>
public class ClickTrackingDto
{
    /// <summary>
    /// Gets or sets a value indicating whether click tracking is enabled.
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether click tracking text is enabled.
    /// </summary>
    public bool EnableText { get; set; }
}
