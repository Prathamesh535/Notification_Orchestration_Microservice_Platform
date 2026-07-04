// -----------------------------------------------------------------------
// <copyright file="WaAttachmentDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Wa;

/// <summary>
/// Represents whatsapp attachment class.
/// </summary>
public class WaAttachmentDto
{
    /// <summary>
    /// Gets or sets whatsapp attachment type.
    /// </summary>
    public Core.Models.Wa.WaAttachmentType Type { get; set; }

    /// <summary>
    /// Gets or sets whatsapp attachment data. It can be base64 content.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets whatsapp attachment URL.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets file name for the attachment.
    /// </summary>
    public string FileName { get; set; }
}
