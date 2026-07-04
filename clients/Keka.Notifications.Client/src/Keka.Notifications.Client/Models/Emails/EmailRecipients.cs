// -----------------------------------------------------------------------
// <copyright file="EmailRecipientsDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Client.Models.Emails;

/// <summary>
/// Represents email recipients Class.
/// </summary>
public class EmailRecipients
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRecipients"/> class.
    /// </summary>
    public EmailRecipients()
    {
        this.To = new List<EmailAddress>();
        this.Cc = new List<EmailAddress>();
        this.Bcc = new List<EmailAddress>();
    }

    /// <summary>
    /// Gets or sets the 'to' recipients.
    /// </summary>
    public IEnumerable<EmailAddress> To { get; set; }

    /// <summary>
    /// Gets or sets the 'cc' recipients.
    /// </summary>
    public IEnumerable<EmailAddress> Cc { get; set; }

    /// <summary>
    /// Gets or sets the 'bcc' recipients.
    /// </summary>
    public IEnumerable<EmailAddress> Bcc { get; set; }
}
   