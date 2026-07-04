// -----------------------------------------------------------------------
// <copyright file="PushNotificationTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.PushNotifications;

/// <summary>
/// Represents an push notification template containing information such as name, title, body, parameters, etc.
/// </summary>
public class PushNotificationTemplate
{
    /// <summary>
    /// Gets or sets the unique identifier for the push notification template.
    /// </summary>
    public Guid PushNotificationTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name of push notification template.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets title of the push notification template.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets body of the push notification template.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Gets or sets template parameters of the push notification template.
    /// </summary>
    public List<string> TemplateParameters { get; set; }

    /// <summary>
    /// Gets or sets data parameters of the push notification template.
    /// </summary>
    public List<string> DataParameters { get; set; }
}