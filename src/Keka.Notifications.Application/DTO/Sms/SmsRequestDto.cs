// -----------------------------------------------------------------------
// <copyright file="SmsRequestDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Sms;

/// <summary>
/// Represents Sms Request Dto.
/// </summary>
public class SmsRequestDto
{
    /// <summary>
    /// Gets or Sets Sms Template Id.
    /// </summary>
#nullable enable
    public Guid? SmsTemplateId { get; set; }

    /// <summary>
    /// Gets or Sets Sms Template Name.
    /// </summary>
    public string? SmsTemplateName { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the list of personalization.
    /// </summary>
    [Required(ErrorMessage = "Personalization is required.")]
    public List<SmsPersonalizationDto> Personalization { get; set; }
}
