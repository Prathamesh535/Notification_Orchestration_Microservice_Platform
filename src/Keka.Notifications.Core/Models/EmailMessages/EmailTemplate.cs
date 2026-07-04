// -----------------------------------------------------------------------
// <copyright file="EmailTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents an email template.
/// </summary>
public class EmailTemplate
{
    /// <summary>
    /// Gets or sets the email template id.
    /// </summary>
    public Guid EmailTemplateId { get; set; }

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

    /// <summary>
    /// Gets or sets created by.
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets created on.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

#nullable enable
    /// <summary>
    /// Gets or sets updated by.
    /// </summary>
    public Guid? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets updated on.
    /// </summary>
    public DateTime? UpdatedOn { get; set; }

    /// <summary>
    /// Gets or sets deleted by.
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Gets or sets deleted on.
    /// </summary>
    public DateTime? DeletedOn { get; set; }
#nullable restore
}