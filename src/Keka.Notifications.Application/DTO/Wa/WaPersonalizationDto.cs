// -----------------------------------------------------------------------
// <copyright file="WaPersonalizationDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Wa;

/// <summary>
/// Represents WhatsApp recipient list with associated template data.
/// </summary>
public class WaPersonalizationDto
{
    /// <summary>
    /// Gets or sets list of recipients.
    /// </summary>
    [Required(ErrorMessage = "Recipients is required.")]
    public List<string> Recipients { get; set; }

    /// <summary>
    /// Gets or sets the template data associated with each recipient in the recipient list.
    /// </summary>
    public Dictionary<string, string> TemplateData { get; set; }

    /// <summary>
    /// Gets or sets attachments.
    /// </summary>
    public List<WaAttachmentDto> Attachments { get; set; }
}
