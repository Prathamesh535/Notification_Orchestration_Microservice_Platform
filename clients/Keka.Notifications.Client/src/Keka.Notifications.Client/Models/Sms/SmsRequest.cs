// -----------------------------------------------------------------------
// <copyright file="SmsRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.Sms;

/// <summary>
/// Represents an Sms Request class.
/// </summary>
public class SmsRequest
{
    /// <summary>
    /// Gets or Sets Sms Template Id.
    /// </summary>
#nullable enable
    public Guid? SmsTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name.
    /// </summary>
    public string? SmsTemplateName { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the list of personalization.
    /// </summary>
    public List<SmsPersonalization> Personalization { get; set; }
}
