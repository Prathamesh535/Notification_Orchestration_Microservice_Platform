// -----------------------------------------------------------------------
// <copyright file="EmailAddress.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents an email address with an optional name.
/// </summary>
public class EmailAddress
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddress"/> class.
    /// </summary>
    public EmailAddress()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddress"/> class with the specified address.
    /// </summary>
    /// <param name="address">The email address.</param>
    public EmailAddress(string address)
    {
        this.Address = address;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddress"/> class with the specified address and display name.
    /// </summary>
    /// <param name="address">The email address.</param>
    /// <param name="displayName">The display name.</param>
    public EmailAddress(string address, string displayName)
    {
        this.Address = address;
        this.DisplayName = displayName;
    }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets the mail address in the format "DisplayName &lt;Address&gt;" if the display name is not empty; otherwise, returns the address.
    /// </summary>
    [JsonIgnore]
    public string MailAddress => string.IsNullOrWhiteSpace(this.DisplayName) ? this.Address : $"{this.DisplayName} <{this.Address}>";
}