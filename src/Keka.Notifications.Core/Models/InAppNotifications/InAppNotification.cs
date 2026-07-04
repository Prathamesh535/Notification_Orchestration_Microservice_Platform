// -----------------------------------------------------------------------
// <copyright file="InAppNotification.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.InAppNotifications;

/// <summary>
/// Represents an in app notification.
/// </summary>
public class InAppNotification
{
    /// <summary>
  /// Gets or sets the employeeid.
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the in app notification identifier.
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

    /// <summary>
    /// Gets or sets the expires at.
    /// </summary>
    public DateTime? ExpiresAt { get; set; }
}