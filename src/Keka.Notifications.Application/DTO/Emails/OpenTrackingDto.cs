// -----------------------------------------------------------------------
// <copyright file="OpenTrackingDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Represents the email open tracking data transfer object.
/// </summary>
public class OpenTrackingDto
{
    /// <summary>
    /// Gets or sets a value indicating whether open tracking is enabled.
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// Gets or sets the substitution tag for open tracking.
    /// </summary>
    public string SubstitutionTag { get; set; }
}
