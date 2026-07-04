// -----------------------------------------------------------------------
// <copyright file="WaMessageRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.Wa;

/// <summary>
/// Represents whatsapp message class.
/// </summary>
public class WaMessageRequest
{
    /// <summary>
    /// Gets or sets template identifier.
    /// </summary>
#nullable enable
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets template name.
    /// </summary>
    public string? TemplateName { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the list of personalization's for the wa message.
    /// </summary>
    public List<WaPersonalization> Personalization { get; set; }
}
