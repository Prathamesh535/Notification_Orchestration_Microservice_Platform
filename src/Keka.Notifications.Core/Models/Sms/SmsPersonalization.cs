// -----------------------------------------------------------------------
// <copyright file="SmsPersonalization.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Sms;

/// <summary>
/// Represents Sms Personalization class.
/// </summary>
public class SmsPersonalization
{
    /// <summary>
    /// Gets or sets List of Recipients.
    /// </summary>
    public List<string> Recipients { get; set; }

    /// <summary>
    /// Gets or sets Template Data Recipient List.
    /// </summary>
    public Dictionary<string, string> TemplateData { get; set; }
}
