// -----------------------------------------------------------------------
// <copyright file="EmailSendResponse.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents email send response class.
/// </summary>
public class EmailSendResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailSendResponse"/> class.
    /// </summary>
    public EmailSendResponse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailSendResponse"/> class.
    /// </summary>
    /// <param name="status">Notification sent status.</param>
    /// <param name="externalId">Unique identifer of the external system.</param>
    public EmailSendResponse(NotificationStatus status, string externalId)
    {
        this.Status = status;
        this.ExternalId = externalId;
    }

    /// <summary>
    /// Gets or sets notification sending status.
    /// </summary>
    public NotificationStatus Status { get; set; }

    /// <summary>
    /// Gets or sets unique identifer of the external system.
    /// </summary>
    public string ExternalId { get; set; }
}
