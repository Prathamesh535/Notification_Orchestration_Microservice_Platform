// -----------------------------------------------------------------------
// <copyright file="NotificationChannel.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models;

/// <summary>
/// Represents an enum for notification channel.
/// </summary>
public enum NotificationChannel : short
{
    /// <summary>
    /// No action type specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Represents an Email action.
    /// </summary>
    Email = 1,

    /// <summary>
    /// Represents an SMS action.
    /// </summary>
    SMS = 2,

    /// <summary>
    /// Represents a Push notification action.
    /// </summary>
    Push = 3,

    /// <summary>
    /// Represents a WebHook action.
    /// </summary>
    WebHook = 4,

    /// <summary>
    /// Represents posting content on a wall (e.g., social media wall or internal message wall).
    /// </summary>
    PostOnWall = 5,

    /// <summary>
    /// Represents a task setup action.
    /// </summary>
    TaskSetup = 6,

    /// <summary>
    /// Represents a Slack message action.
    /// </summary>
    Slack = 7,

    /// <summary>
    /// Represents a whatsapp message action.
    /// </summary>
    WhatsApp = 8,

    /// <summary>
    /// Represents any other action type not covered by specific values.
    /// </summary>
    Other = 99,
}