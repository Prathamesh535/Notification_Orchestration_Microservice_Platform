// -----------------------------------------------------------------------
// <copyright file="InAppNotificationPersonalizationDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.InAppNotification;

/// <summary>
/// Represents the in app notification personalization data transfer object.
/// </summary>
public class InAppNotificationPersonalizationDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InAppNotificationPersonalizationDto"/> class.
    /// </summary>
    public InAppNotificationPersonalizationDto()
    {
        this.Recipients = new List<Guid>();

        this.TemplateData = new Dictionary<string, string>();

        this.MetaData = new Dictionary<string, object>();
    }

    /// <summary>
    /// Gets or sets the recipients.
    /// </summary>
    [Required(ErrorMessage = "Recipients is required.")]
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
