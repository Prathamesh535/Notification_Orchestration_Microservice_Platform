// -----------------------------------------------------------------------
// <copyright file="EmailDeliveryHistory.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents email delivery history class.
/// </summary>
public class EmailDeliveryHistory
{
    /// <summary>
    /// Gets or sets the source email address.
    /// </summary>
    public string FromEmail { get; set; }

    /// <summary>
    /// Gets or sets the email subject.
    /// </summary>
    public string Subject { get; set; }

#nullable enable
    /// <summary>
    /// Gets or sets the sent on time.
    /// </summary>
    public DateTime? SentOn { get; set; }

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the delivery status.
    /// </summary>
    public EmailDeliveryStatus? DeliveryStatus { get; set; }

    /// <summary>
    /// Gets or sets theTenant Id.
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Gets or sets the Updated on time.
    /// </summary>
    public DateTime? UpdatedOn { get; set; }

    /// <summary>
    /// Gets or sets the Failed reason.
    /// </summary>
    public EmailFailedReason? FailedReason { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email has any attachments.
    /// </summary>
    public bool? HasAttachment { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the To Email.
    /// </summary>
    public string ToEmail { get; set; }

    /// <summary>
    /// Gets or sets the External Id.
    /// </summary>
    public string ExternalId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    public DateTime? Timestamp { get; set; }
}