// -----------------------------------------------------------------------
// <copyright file="EmailRecipients.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents a collection of email recipients.
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
    /// Initializes a new instance of the <see cref="EmailRecipients"/> class with the specified 'to' recipients.
    /// </summary>
    /// <param name="to">The 'to' recipients.</param>
    public EmailRecipients(IEnumerable<EmailAddress> to)
    {
        this.To = to;
        this.Cc = new List<EmailAddress>();
        this.Bcc = new List<EmailAddress>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRecipients"/> class with the specified 'to' and 'cc' recipients.
    /// </summary>
    /// <param name="to">The 'to' recipients.</param>
    /// <param name="cc">The 'cc' recipients.</param>
    public EmailRecipients(IEnumerable<EmailAddress> to, IEnumerable<EmailAddress> cc)
    {
        this.To = to;
        this.Cc = cc;
        this.Bcc = new List<EmailAddress>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRecipients"/> class with the specified 'to', 'cc', and 'bcc' recipients.
    /// </summary>
    /// <param name="to">The 'to' recipients.</param>
    /// <param name="cc">The 'cc' recipients.</param>
    /// <param name="bcc">The 'bcc' recipients.</param>
    public EmailRecipients(IEnumerable<EmailAddress> to, IEnumerable<EmailAddress> cc, IEnumerable<EmailAddress> bcc)
    {
        this.To = to;
        this.Cc = cc;
        this.Bcc = bcc;
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