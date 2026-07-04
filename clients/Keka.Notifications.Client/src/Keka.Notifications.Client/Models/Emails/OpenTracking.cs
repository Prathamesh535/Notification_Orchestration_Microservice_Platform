// -----------------------------------------------------------------------
// <copyright file="OpenTracking.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Client.Models.Emails;

/// <summary>
/// Represents the email open tracking data transfer object.
/// </summary>
public class OpenTracking
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
