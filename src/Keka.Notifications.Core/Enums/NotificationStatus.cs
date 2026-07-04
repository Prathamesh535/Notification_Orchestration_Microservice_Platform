// -----------------------------------------------------------------------
// <copyright file="NotificationStatus.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Enums;

/// <summary>
/// Represents an enum for status.
/// </summary>
public enum NotificationStatus : short
{
    /// <summary>
    /// Indicates notification is queued for sending.
    /// </summary>
    Queued = 0,

    /// <summary>
    /// Indicates notification is enriched.
    /// </summary>
    Enriched = 1,

    /// <summary>
    /// Indicates notification is sent successfully.
    /// </summary>
    Sent = 2,

    /// <summary>
    /// Indicates notification is failed while sending.
    /// </summary>
    Failed = 3,
}