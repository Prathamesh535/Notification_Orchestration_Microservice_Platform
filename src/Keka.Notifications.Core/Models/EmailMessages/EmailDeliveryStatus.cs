// -----------------------------------------------------------------------
// <copyright file="EmailDeliveryStatus.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents the email delivery status of an email.
/// </summary>
public enum EmailDeliveryStatus : short
{
    /// <summary>
    /// Indicates email delivery status is none.
    /// </summary>
    [System.ComponentModel.Description("NA")]
    None = 0,

    /// <summary>
    /// Indicates email delivery status is delivered.
    /// </summary>
    [System.ComponentModel.Description("Delivered")]
    Delivery = 1,

    /// <summary>
    /// Indicates email delivery status is bounce.
    /// </summary>
    [System.ComponentModel.Description("Bounce")]
    Bounce = 2,

    /// <summary>
    /// Indicates email delivery status is complaint.
    /// </summary>
    [System.ComponentModel.Description("Complaint")]
    Complaint = 3,
}