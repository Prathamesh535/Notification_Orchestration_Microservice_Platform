// -----------------------------------------------------------------------
// <copyright file="NotificationType.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Enums;

/// <summary>
/// Represents an enum for notification type.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Indicates email notification type.
    /// </summary>
    Emails,

    /// <summary>
    /// Indicates sms notification type.
    /// </summary>
    Sms,

    /// <summary>
    /// Indicates push notification type.
    /// </summary>
    Push,

    /// <summary>
    /// Indicates web hook notification type.
    /// </summary>
    Webhooks,

    /// <summary>
    /// Indicates slack notification type.
    /// </summary>
    Slack,

    /// <summary>
    /// Indicates in app notification type.
    /// </summary>
    InApp,

    /// <summary>
    /// Indicates whatsapp notification type.
    /// </summary>
    Whatsapp,
}
