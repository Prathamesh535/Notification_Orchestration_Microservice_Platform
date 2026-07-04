// -----------------------------------------------------------------------
// <copyright file="InAppNotificationPersonalization.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.InAppNotifications;

/// <summary>
/// Represents the in notification personalization.
/// </summary>
public class InAppNotificationPersonalization
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InAppNotificationPersonalization"/> class.
    /// </summary>
    public InAppNotificationPersonalization()
    {
        this.Recipients = new List<Guid>();

        this.TemplateData = new Dictionary<string, string>();

        this.MetaData = new Dictionary<string, object>();
    }

    /// <summary>
    /// Gets or sets list of recipients.
    /// </summary>
    public List<Guid> Recipients { get; set; }

    /// <summary>
    /// Gets or sets the template data.
    /// </summary>
    public Dictionary<string, string> TemplateData { get; set; }

    /// <summary>
    /// Gets or sets the meta data.
    /// </summary>
    /// <value>
    /// The meta data.
    /// </value>
    public Dictionary<string, object> MetaData { get; set; }

    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    public string Url { get; set; }
}
