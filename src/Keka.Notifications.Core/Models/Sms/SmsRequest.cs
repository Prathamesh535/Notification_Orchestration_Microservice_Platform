// -----------------------------------------------------------------------
// <copyright file="SmsRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Sms;

/// <summary>
/// Represents an Sms Request class.
/// </summary>
public class SmsRequest
{
    /// <summary>
    /// Gets or sets Sms Request Id.
    /// </summary>
    public Guid SmsRequestId { get; set; }

    /// <summary>
    /// Gets or sets Sms Template Identifier.
    /// </summary>
    public Guid? SmsTemplateId { get; set; }

    /// <summary>
    /// Gets or sets List of Personalization.
    /// </summary>
    public List<SmsPersonalization> Personalization { get; set; }

    /// <summary>
    /// Gets or sets Sms Status.
    /// </summary>
    public NotificationStatus Status { get; set; }

    #nullable enable
    /// <summary>
    /// Gets or sets External Id.
    /// </summary>
    public string? ExternalId { get; set; }

    /// <summary>
    /// Gets or sets failure reason.
    /// </summary>
    public string? FailureReason { get; set; }
    #nullable restore
}