// -----------------------------------------------------------------------
// <copyright file="EmailDeliveryRawDataBasicDetailsDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Represents class related to email delivery raw event data response.
/// </summary>
public class EmailDeliveryRawDataBasicDetailsDto
{
    /// <summary>
    /// Gets or sets the timestamp of the event.
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Gets or sets raw response of the event.
    /// </summary>
    public string RawResponse { get; set; }
}
