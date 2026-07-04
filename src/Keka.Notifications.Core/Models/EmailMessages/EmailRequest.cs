// -----------------------------------------------------------------------
// <copyright file="EmailRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents an email request.
/// </summary>
public class EmailRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRequest"/> class.
    /// </summary>
    public EmailRequest()
    {
        this.Personalization = new List<EmailPersonalization>();
        this.Attachments = new List<EmailAttachment>();
        this.TrackingSettings = new EmailTrackingSettings();
        this.ReplyTo = new List<EmailAddress>();
    }

    /// <summary>
    /// Gets or sets the email request identifier.
    /// </summary>
    public Guid EmailRequestId { get; set; }

    /// <summary>
    /// Gets or sets the list of personalization's for the email message.
    /// </summary>
    public List<EmailPersonalization> Personalization { get; set; }

    /// <summary>
    /// Gets or sets the sender's email address.
    /// </summary>
    public EmailAddress From { get; set; }

    /// <summary>
    /// Gets or sets the reply-to email address.
    /// </summary>
    public List<EmailAddress> ReplyTo { get; set; }

    /// <summary>
    /// Gets or sets the list of email content.
    /// </summary>
    public EmailContent Content { get; set; }

    /// <summary>
    /// Gets or sets the list of email attachments.
    /// </summary>
    public List<EmailAttachment> Attachments { get; set; }

    /// <summary>
    /// Gets or sets the template ID for the email message.
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp to schedule the email message for sending.
    /// </summary>
    public DateTime? SendAt { get; set; }

    /// <summary>
    /// Gets or sets the tracking settings for the email message.
    /// </summary>
    public EmailTrackingSettings TrackingSettings { get; set; }
}