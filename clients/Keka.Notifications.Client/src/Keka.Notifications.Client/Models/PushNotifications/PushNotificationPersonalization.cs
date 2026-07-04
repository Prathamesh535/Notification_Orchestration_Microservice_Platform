// -----------------------------------------------------------------------
// <copyright file="PushNotificationPersonalization.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.PushNotifications;

/// <summary>
/// Represents the personalization details for push notification.
/// </summary>
public class PushNotificationPersonalization
{
    /// <summary>
    /// Gets or sets array of recipients.
    /// </summary>
    public List<string> Recipients { get; set; }

    /// <summary>
    /// Gets or sets template data for above recipient list.
    /// </summary>
    public Dictionary<string, string> TemplateData { get; set; }

    /// <summary>
    /// Gets or sets data required for the push notification request action.
    /// </summary>
    public Dictionary<string, string> Data { get; set; }
}
