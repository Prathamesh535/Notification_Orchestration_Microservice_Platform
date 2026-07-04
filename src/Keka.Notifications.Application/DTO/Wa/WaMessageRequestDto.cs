// -----------------------------------------------------------------------
// <copyright file="WaMessageRequestDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Keka.Notifications.Application.DTO.Wa;

/// <summary>
/// Represents whatsapp message DTO.
/// </summary>
public class WaMessageRequestDto
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
    [Required(ErrorMessage = "Personalization is required field")]
    public List<WaPersonalizationDto> Personalization { get; set; }
}