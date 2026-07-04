// -----------------------------------------------------------------------
// <copyright file="SqsMessageDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.SQSEvents;

/// <summary>
/// Represents the message details within the SQS event body.
/// </summary>
public class SqsMessageDto
{
    /// <summary>
    /// Gets or sets the notification type.
    /// </summary>
    public string NotificationType { get; set; }

    /// <summary>
    /// Gets or sets the event type.
    /// </summary>
    public string EventType { get; set; }

    /// <summary>
    /// Gets or sets the complaint details.
    /// </summary>
    public SqsComplaintDto Complaint { get; set; }

    /// <summary>
    /// Gets or sets the bounce object.
    /// </summary>
    public SqsBounceDto Bounce { get; set; }

    /// <summary>
    /// Gets or sets the mail details.
    /// </summary>
    public SqsMailDto Mail { get; set; }

    /// <summary>
    /// Gets or sets the delivery details.
    /// </summary>
    public SqsDeliveryDto Delivery { get; set; }

    /// <summary>
    /// Gets event type.
    /// </summary>
    /// <returns>Event type.</returns>
    public string GetEventType()
    {
        return (this.EventType ?? this.NotificationType).ToLower();
    }
}
