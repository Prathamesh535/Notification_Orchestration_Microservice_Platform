// -----------------------------------------------------------------------
// <copyright file="EmailTrackingSettings.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents the tracking settings for an email.
/// </summary>
public class EmailTrackingSettings
{
    /// <summary>
    /// Gets or sets the click tracking settings.
    /// </summary>
    public ClickTracking ClickTracking { get; set; }

    /// <summary>
    /// Gets or sets the open tracking settings.
    /// </summary>
    public OpenTracking OpenTracking { get; set; }
}