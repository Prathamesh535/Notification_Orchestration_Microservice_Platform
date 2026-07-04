// -----------------------------------------------------------------------
// <copyright file="EmailFailedReason.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents email failed reason.
/// </summary>
public enum EmailFailedReason : short
{
    /// <summary>
    /// None
    /// </summary>
    [System.ComponentModel.Description("Unknown")]
    Unknown = 0,

    /// <summary>
    /// Indicates email bounce with reason attachment rejected.
    /// </summary>
    [System.ComponentModel.Description("Bounce Attachment Rejected")]
    BounceAttachmentRejected = 1,

    /// <summary>
    /// Indicates permanant bounce.
    /// </summary>
    [System.ComponentModel.Description("Bounce Permanent")]
    BouncePermanent = 2,

    /// <summary>
    /// Indicates permanant bounce.
    /// </summary>
    [System.ComponentModel.Description("Bounce Transient")]
    BounceTransient = 3,

    /// <summary>
    /// Indicates email complaint.
    /// </summary>
    [System.ComponentModel.Description("Email Complaint")]
    EmailComplaint = 4,

    /// <summary>
    /// Indicates invalid email domain.
    /// </summary>
    [System.ComponentModel.Description("Invalid Email Domain")]
    InvalidEmailDomain = 5,

    /// <summary>
    /// Indicates account is on supression list.
    /// </summary>
    [System.ComponentModel.Description("Account On SupressionList")]
    AccountOnSuppressionList = 6,
}
