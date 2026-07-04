// -----------------------------------------------------------------------
// <copyright file="EmailStatusPreviousStateDetails.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represetns email status previous state details.
/// </summary>
public class EmailStatusPreviousStateDetails
{
    /// <summary>
    /// Gets or sets the date and time when the last email was successfully delivered.
    /// </summary>
    public DateTime? LastEmailDeliveredOn { get; set; }

    /// <summary>
    /// Gets or sets the event external Id.
    /// </summary>
    public string MessageExternalId { get; set; }

    /// <summary>
    /// Gets or sets the consecutive block count.
    /// </summary>
    public short ConsecutiveBlockCount { get; set; }
}