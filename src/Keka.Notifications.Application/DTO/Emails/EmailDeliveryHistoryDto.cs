// -----------------------------------------------------------------------
// <copyright file="EmailDeliveryHistoryDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Represents the email history personalization data transfer object.
/// </summary>
public class EmailDeliveryHistoryDto
{
    /// <summary>
    /// Gets or sets from mail.
    /// </summary>
    /// <value>
    /// From mail.
    /// </value>
    public string FromEmail { get; set; }

    /// <summary>
    /// Gets or sets the sent on.
    /// </summary>
    /// <value>
    /// The sent on.
    /// </value>
    public DateTime? SentOn { get; set; }

    /// <summary>
    /// Gets or sets the subject.
    /// </summary>
    /// <value>
    /// The subject.
    /// </value>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the delivery status id.
    /// </summary>
    /// <value>
    /// The delivery status id.
    /// </value>
    public EmailDeliveryStatus? DeliveryStatusId { get; set; }

    /// <summary>
    /// Gets the delivery status.
    /// </summary>
    /// <value>
    /// The delivery status.
    /// </value>
    public string DeliveryStatus
    {
        get
        {
            return this.DeliveryStatusId?.GetDescription();
        }
    }

    /// <summary>
    /// Gets or sets the time stamp.
    /// </summary>
    /// <value>
    /// The time stamp.
    /// </value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the Failed reason id.
    /// </summary>
    public EmailFailedReason? FailedReasonId { get; set; }

    /// <summary>
    /// Gets the Failed reason.
    /// </summary>
    public string FailedReason
    {
        get
        {
            return this.FailedReasonId?.GetDescription();
        }
    }

    /// <summary>
    /// Gets the delivery description.
    /// </summary>
    /// <value>
    /// The delivery description.
    /// </value>
    public string DeliveryDescription
    {
        get
        {
            if (this.FailedReasonId.HasValue && this.FailedReasonId.Equals(EmailFailedReason.AccountOnSuppressionList))
            {
                return "Message Not Sent";
            }

            if (this.DeliveryStatusId is null)
            {
                return "Sent";
            }
            else if (this.DeliveryStatusId == EmailDeliveryStatus.Delivery)
            {
                return "Message Delivered";
            }

            return "Delivery Failed";
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the email has any attachments.
    /// </summary>
    public bool? HasAttachment { get; set; }

    /// <summary>
    /// Gets or sets the message id.
    /// </summary>
    public string ExternalId { get; set; }
}