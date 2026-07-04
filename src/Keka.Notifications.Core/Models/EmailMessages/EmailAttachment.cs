// -----------------------------------------------------------------------
// <copyright file="EmailAttachment.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents an email attachment.
/// </summary>
public class EmailAttachment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    public EmailAttachment()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the attachment.</param>
    /// <param name="filePath">The File Name.</param>
    public EmailAttachment(string name, string filePath)
    {
        this.Name = name;
        this.FilePath = filePath;
    }

    /// <summary>
    /// Gets or sets the name of the attachment.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the content of the attachment.
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// Gets or sets the content type of the attachment.
    /// </summary>
    public string ContentType { get; set; }
}