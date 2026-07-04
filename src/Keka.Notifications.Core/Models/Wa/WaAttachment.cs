// -----------------------------------------------------------------------
// <copyright file="WaAttachment.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Wa;

/// <summary>
/// Represents whatsapp attachment class.
/// </summary>
public class WaAttachment
{
    /// <summary>
    /// Gets or sets whatsapp attachment type.
    /// </summary>
    public WaAttachmentType Type { get; set; }

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
