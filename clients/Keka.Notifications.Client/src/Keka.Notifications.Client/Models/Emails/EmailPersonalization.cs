// -----------------------------------------------------------------------
// <copyright file="EmailPersonalization.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Client.Models.Emails;

/// <summary>
/// Represents the email personalization data transfer object.
/// </summary>
public class EmailPersonalization
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailPersonalization"/> class.
    /// </summary>
    public EmailPersonalization()
    {
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

    /// <summary>
    /// Gets or sets the timestamp at which the email should be sent.
    /// </summary>
    public long? SendAt { get; set; }
}
