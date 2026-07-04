// -----------------------------------------------------------------------
// <copyright file="WaTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Wa;

/// <summary>
/// Represents a WhatsApp message template.
/// </summary>
public class WaTemplate
{
    /// <summary>
    /// Gets or sets the unique identifier for the WhatsApp template.
    /// </summary>
    public Guid WaTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the name of the WhatsApp template.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the language of the WhatsApp template.
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// Gets or sets the value of external template id.
    /// </summary>
    public string ExternalTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the parameter mapping.
    /// </summary>
    public Dictionary<string, string> ParameterMapping { get; set; }
}