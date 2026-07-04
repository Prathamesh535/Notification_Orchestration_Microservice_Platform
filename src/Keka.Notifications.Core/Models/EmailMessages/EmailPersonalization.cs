// -----------------------------------------------------------------------
// <copyright file="EmailPersonalization.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents the personalization details for an email.
/// </summary>
public class EmailPersonalization
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailPersonalization"/> class.
    /// </summary>
    public EmailPersonalization()
    {
        this.EmailRecipients = new EmailRecipients();
        this.DynamicTemplateData = new Dictionary<string, string>();
    }

    /// <summary>
    /// Gets or sets the email recipients.
    /// </summary>
    public EmailRecipients EmailRecipients { get; set; }

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the dynamic template data for the email.
    /// </summary>
    public Dictionary<string, string> DynamicTemplateData { get; set; }
}