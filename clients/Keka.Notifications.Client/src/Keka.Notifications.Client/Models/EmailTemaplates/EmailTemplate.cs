// -----------------------------------------------------------------------
// <copyright file="EmailTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.EmailTemplates;

/// <summary>
/// Represents an email template Class.
/// </summary>
public class EmailTemplate
{
    /// <summary>
    /// Gets or sets the email template identifier.
    /// </summary>
    public Guid EmailTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name.
    /// </summary>
    public string TemplateName { get; set; }

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