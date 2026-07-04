// -----------------------------------------------------------------------
// <copyright file="InAppNotificationTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.InAppNotifications;

/// <summary>
/// Represents an in app notification template.
/// </summary>
public class InAppNotificationTemplate
{
    /// <summary>
    /// Gets or sets the in application notification template identifier.
    /// </summary>
    public Guid InAppNotificationTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the body.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Gets or sets the parameters.
    /// </summary>
    public List<string> Parameters { get; set; }
}