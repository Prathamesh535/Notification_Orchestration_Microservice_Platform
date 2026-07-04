// -----------------------------------------------------------------------
// <copyright file="SmsTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Sms;

/// <summary>
/// Represents an Sms Template class.
/// </summary>
public class SmsTemplate
{
    /// <summary>
    /// Gets or sets the template Id.
    /// </summary>
    public Guid SmsTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the Template Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the External Template Id.
    /// </summary>
    public string ExternalTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the Parameter Mapping.
    /// </summary>
    public Dictionary<string, string> ParameterMapping { get; set; }
}
