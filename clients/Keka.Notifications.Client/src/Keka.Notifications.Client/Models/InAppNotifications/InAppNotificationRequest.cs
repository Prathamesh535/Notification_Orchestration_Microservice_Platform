// -----------------------------------------------------------------------
// <copyright file="InAppNotificationRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.InAppNotifications;

/// <summary>
/// Represents an in app notification request.
/// </summary>
public class InAppNotificationRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InAppNotificationRequest"/> class.
    /// </summary>
    public InAppNotificationRequest()
    {
        this.Personalization = new List<InAppNotificationPersonalization>();
    }

    /// <summary>
    /// Gets or sets the template ID.
    /// </summary>
#nullable enable
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name.
    /// </summary>
    public string? TemplateName { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the list of personalization.
    /// </summary>
    public List<InAppNotificationPersonalization> Personalization { get; set; }
}