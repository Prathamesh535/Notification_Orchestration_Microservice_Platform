// -----------------------------------------------------------------------
// <copyright file="Email.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents an email request containing information such as sender, recipients, content, attachments, etc.
/// </summary>
public class Email
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    public Email()
    {
        this.ReplyTo = new List<EmailAddress>();
        this.Attachments = new List<EmailAttachment>();
    }

    /// <summary>
    /// Gets or sets the unique identifier for the email request.
    /// </summary>
    public Guid EmailId { get; set; }

    /// <summary>
    /// Gets or sets the sender's email address.
    /// </summary>
    public EmailAddress From { get; set; }

    /// <summary>
    /// Gets or sets the recipients of the email.
    /// </summary>
    public EmailRecipients Recipients { get; set; }

    /// <summary>
    /// Gets or sets the content of the email.
    /// </summary>
    public EmailContent Content { get; set; }

    /// <summary>
    /// Gets or sets the list of email addresses to reply to.
    /// </summary>
    public IEnumerable<EmailAddress> ReplyTo { get; set; }

    /// <summary>
    /// Gets or sets the list of email attachments.
    /// </summary>
    public IEnumerable<EmailAttachment> Attachments { get; set; }

    /// <summary>
    /// Gets or sets request id of the email.
    /// </summary>
    public string EmailRequestId { get; set; }

    /// <summary>
    /// Gets or sets external id of the email. This represents id from the external system such as Amazon SNS.
    /// </summary>
    public string ExternalId { get; set; }

    /// <summary>
    /// Gets or sets email sending status.
    /// </summary>
    public NotificationStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the eamil sent on time.
    /// </summary>
    public DateTime SentOn { get; set; }

    /// <summary>
    /// Adds the specified content to the email request.
    /// </summary>
    /// <param name="content">The email content.</param>
    /// <returns>The updated email request.</returns>
    public Email AddContent(EmailContent content)
    {
        this.Content = content;
        return this;
    }

    /// <summary>
    /// Adds the specified sender to the email request.
    /// </summary>
    /// <param name="from">The sender's email address.</param>
    /// <returns>The updated email request.</returns>
    public Email AddFrom(EmailAddress from)
    {
        this.From = from;
        return this;
    }

    /// <summary>
    /// Adds the specified recipients to the email request.
    /// </summary>
    /// <param name="recipients">The recipients of the email.</param>
    /// <returns>The updated email request.</returns>
    public Email AddRecipients(EmailRecipients recipients)
    {
        this.Recipients = recipients;
        return this;
    }

    /// <summary>
    /// Adds the specified email addresses to the list of email addresses to reply to.
    /// </summary>
    /// <param name="replyTo">The email addresses to reply to.</param>
    /// <returns>The updated email request.</returns>
    public Email AddReplyTo(IEnumerable<EmailAddress> replyTo)
    {
        this.ReplyTo = replyTo;
        return this;
    }

    /// <summary>
    /// Adds the specified email attachments to the email request.
    /// </summary>
    /// <param name="attachments">The email attachments.</param>
    /// <returns>The updated email request.</returns>
    public Email AddAttachments(IEnumerable<EmailAttachment> attachments)
    {
        this.Attachments = attachments;
        return this;
    }
}