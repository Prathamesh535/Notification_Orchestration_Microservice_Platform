// -----------------------------------------------------------------------
// <copyright file="ClickTracking.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.Emails;

/// <summary>
/// Class to represent the email click tracking data transfer object.
/// </summary>
public class ClickTracking
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
