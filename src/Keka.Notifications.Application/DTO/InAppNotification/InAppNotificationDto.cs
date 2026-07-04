// -----------------------------------------------------------------------
// <copyright file="InAppNotificationDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.InAppNotification;

/// <summary>
/// Represents an in app notification data transfer object.
/// </summary>
public class InAppNotificationDto
{
    /// <summary>
    /// Gets or sets the in app notification request identifier.
    /// </summary>
    public string InAppNotificationId { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the body.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Gets or sets the meta data.
    /// </summary>
    public IDictionary<string, object> MetaData { get; set; }

    /// <summary>
    /// Gets or sets the url.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is read.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets the read on.
    /// </summary>
    public DateTime? ReadOn { get; set; }

    /// <summary>
    /// Gets or sets the created on.
    /// </summary>
    public DateTime CreatedOn { get; set; }
}