// -----------------------------------------------------------------------
// <copyright file="SmsPersonalizationDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Sms;

/// <summary>
/// Represents Sms Personalization Dto.
/// </summary>
public class SmsPersonalizationDto
{
    /// <summary>
    /// Gets or Sets List of Recipients.
    /// </summary>
    [Required(ErrorMessage = "Recipients is required.")]
    public List<string> Recipients { get; set; }

    /// <summary>
    /// Gets or Sets Template Data for Recipients List.
    /// </summary>
    public Dictionary<string, string> TemplateData { get; set; }
}
