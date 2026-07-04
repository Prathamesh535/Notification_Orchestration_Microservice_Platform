// -----------------------------------------------------------------------
// <copyright file="PushNotificationRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.PushNotifications;

/// <summary>
/// Represents the push notification request Class.
/// </summary>
public class PushNotificationRequest
{
    /// <summary>
    /// Gets or sets the template id.
    /// </summary>
#nullable enable
    public Guid? PushNotificationTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name.
    /// </summary>
    public string? PushNotificationTemplateName { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the personalization data.
    /// </summary>
    public List<PushNotificationPersonalization> Personalization { get; set; }
}
