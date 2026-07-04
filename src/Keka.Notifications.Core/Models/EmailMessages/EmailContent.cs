// -----------------------------------------------------------------------
// <copyright file="EmailContent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents the content of an email.
/// </summary>
public class EmailContent
{
    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the HTML content of the email.
    /// </summary>
    public string Html { get; set; }

    /// <summary>
    /// Gets or sets the plain text content of the email.
    /// </summary>
    public string PlainText { get; set; }
}