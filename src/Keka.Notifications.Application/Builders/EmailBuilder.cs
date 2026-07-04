// -----------------------------------------------------------------------
// <copyright file="EmailBuilder.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Builders;

/// <summary>
/// Represents email builder.
/// </summary>
public class EmailBuilder
{
    private Email email;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailBuilder"/> class.
    /// </summary>
    public EmailBuilder()
    {
        this.email = new Email();
    }

    /// <summary>
    /// Create new email instance.
    /// </summary>
    /// <returns>Returns email builder.</returns>
    public EmailBuilder StartNewEmail()
    {
        this.email = new Email();
        return this;
    }

    /// <summary>
    /// Set from email address.
    /// </summary>
    /// <param name="from">The from email address.</param>
    /// <returns>Returns email builder.</returns>
    public EmailBuilder SetFrom(Core.Models.EmailMessages.EmailAddress from)
    {
        this.email.From = from;
        return this;
    }

    /// <summary>
    /// Set reply to email address.
    /// </summary>
    /// <param name="replyTo">The reply to email address.</param>
    /// <returns>Returns email builder.</returns>
    public EmailBuilder SetReplyTo(IEnumerable<Core.Models.EmailMessages.EmailAddress> replyTo)
    {
        this.email.ReplyTo = replyTo.Any() ? replyTo : new List<Core.Models.EmailMessages.EmailAddress>();
        return this;
    }

    /// <summary>
    /// Set recipients.
    /// </summary>
    /// <param name="emailRecipients">The email email recipients.</param>
    /// <returns>Returns email builder.</returns>
    public EmailBuilder SetRecipients(EmailRecipients emailRecipients)
    {
        this.email.Recipients = emailRecipients;
        return this;
    }

    /// <summary>
    /// Sets the content of the email and returns the EmailBuilder object.
    /// </summary>
    /// <param name="emailContent">The content of the email.</param>
    /// <returns>Returns email builder.</returns>
    public EmailBuilder SetContent(EmailContent emailContent)
    {
        this.email.Content = new EmailContent
        {
            Subject = emailContent.Subject,
            Html = emailContent.Html,
            PlainText = emailContent.PlainText,
        };

        return this;
    }

    /// <summary>
    /// Set email attachments.
    /// </summary>
    /// <param name="attachments">The email attachments.</param>
    /// <returns>Returns email builder.</returns>
    public EmailBuilder SetAttachments(IEnumerable<EmailAttachment> attachments)
    {
        this.email.Attachments = attachments;
        return this;
    }

    /// <summary>
    /// Sets email request id.
    /// </summary>
    /// <param name="emailRequestId">Email request id.</param>
    /// <returns>Returns email builder.</returns>
    public EmailBuilder SetEmailRequestId(string emailRequestId)
    {
        this.email.EmailRequestId = emailRequestId;
        return this;
    }

    /// <summary>
    /// Build email object.
    /// </summary>
    /// <returns>Returns email.</returns>
    public Email Build()
    {
        // Validate the email object or throw exception if necessary
        return this.email;
    }
}