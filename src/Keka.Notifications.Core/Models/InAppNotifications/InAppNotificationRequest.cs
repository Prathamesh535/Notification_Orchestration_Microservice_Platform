// -----------------------------------------------------------------------
// <copyright file="InAppNotificationRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.InAppNotifications;

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
    }

    /// <summary>
    /// Gets or sets the in app notification request identifier.
    /// </summary>
    public string InAppNotificationRequestId { get; set; }

    /// <summary>
    /// Gets or sets the template identifier.
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the list of personalization.
    /// </summary>
    public List<InAppNotificationPersonalization> Personalization { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public NotificationStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the failure reason.
    /// </summary>
    public string FailureReason { get; set; }
}