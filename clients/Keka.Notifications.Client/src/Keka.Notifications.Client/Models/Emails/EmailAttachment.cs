// -----------------------------------------------------------------------
// <copyright file="EmailAttachment.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Client.Models.Emails;

/// <summary>
/// Represents the email attachment data transfer object.
/// </summary>
public class EmailAttachment
{
    /// <summary>
    /// Gets or sets the content of the attachment.
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// Gets or sets the file name of the attachment.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the attachment.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Gets or sets the disposition of the attachment.
    /// </summary>
    public string Disposition { get; set; }
}
