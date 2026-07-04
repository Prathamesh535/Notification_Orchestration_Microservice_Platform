// -----------------------------------------------------------------------
// <copyright file="EmailTemplateUpdateDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.EmailTemplates;

/// <summary>
/// Class to represent the Email Template update data transfer object.
/// </summary>
public class EmailTemplateUpdateDto
{
    /// <summary>
    /// Gets or sets the template name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the subject.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the email body.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Gets or sets the from email address.
    /// </summary>
    public string From { get; set; }

    /// <summary>
    /// Gets or sets the reply to email address.
    /// </summary>
    public string ReplyTo { get; set; }
}