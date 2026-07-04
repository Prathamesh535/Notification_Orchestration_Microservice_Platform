// -----------------------------------------------------------------------
// <copyright file="WaAttachmentType.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Wa;

/// <summary>
/// Enum represents attachment types of whatsapp.
/// </summary>
public enum WaAttachmentType : short
{
    /// <summary>
    /// Indicates none.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates Document attachment type.
    /// </summary>
    Document = 1,

    /// <summary>
    /// Indicates image attachment type.
    /// </summary>
    Image = 2,

    /// <summary>
    /// Indicates video attachment type.
    /// </summary>
    Video = 3,
}
