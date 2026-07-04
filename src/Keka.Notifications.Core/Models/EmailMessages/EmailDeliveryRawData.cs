// -----------------------------------------------------------------------
// <copyright file="EmailDeliveryRawData.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents class related to email delivery raw event data.
/// </summary>
public class EmailDeliveryRawData
{
    /// <summary>
    /// Gets or sets the timestamp of the event.
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Gets or sets unique external id of the email message.
    /// </summary>
    public string ExternalId { get; set; }

    /// <summary>
    /// Gets or sets event type.
    /// </summary>
    public EmailDeliveryStatus? DeliveryStatus { get; set; }

    /// <summary>
    /// Gets or sets raw response of the event.
    /// </summary>
    public string RawResponse { get; set; }

    /// <summary>
    /// Gets or sets md5 checksum of raw response.
    /// </summary>
    public string Md5Checksum { get; set; }
}
