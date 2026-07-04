// -----------------------------------------------------------------------
// <copyright file="SmsPersonalization.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.Sms;

/// <summary>
/// Represents Sms Personalization.
/// </summary>
public class SmsPersonalization
{
    /// <summary>
    /// Gets or Sets List of Recipients.
    /// </summary>
    public List<string> Recipients { get; set; }

    /// <summary>
    /// Gets or Sets Template Data for Recipients List.
    /// </summary>
    public Dictionary<string, string> TemplateData { get; set; }
}
