// -----------------------------------------------------------------------
// <copyright file="PushNotificationRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.PushNotifications;

/// <summary>
/// Represents an push notification request containing information such as template, etc.
/// </summary>
public class PushNotificationRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the push notification request.
    /// </summary>
    public Guid PushNotificationRequestId { get; set; }

    /// <summary>
    /// Gets or sets the push notification template id of push notification request.
    /// </summary>
    public Guid PushNotificationTemplateId { get; set; }

    /// <summary>
    /// Gets or sets personalization of the push notification request.
    /// </summary>
    public List<PushNotificationPersonalization> Personalization { get; set; }

    /// <summary>
    /// Gets or sets push notification sending status.
    /// </summary>
    public NotificationStatus Status { get; set; }

#nullable enable
    /// <summary>
    /// Gets or sets external id of the push notification. This represents id from the external system such as Amazon SNS.
    /// </summary>
    public string? ExternalId { get; set; }

    /// <summary>
    /// Gets or sets push notification request failure reason.
    /// </summary>
    public string? FailureReason { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets CreatedOn.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets CreatedBy.
    /// </summary>
    public Guid CreatedBy { get; set; }
}