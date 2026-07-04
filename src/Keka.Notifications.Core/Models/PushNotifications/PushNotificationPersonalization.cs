// -----------------------------------------------------------------------
// <copyright file="PushNotificationPersonalization.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.PushNotifications;

/// <summary>
/// Represents the personalization details for an push notification.
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
    /// Gets or sets additional data required for the push notification request action.
    /// This data may include links or other information that the mobile app can use
    /// to route to a specific page or perform a specific action.
    /// For example:
    /// - "link": "https://example.com/some-page"
    /// - "action": "openPage"
    /// - "pageId": "12345".
    /// </summary>
    public Dictionary<string, string> Data { get; set; }
}