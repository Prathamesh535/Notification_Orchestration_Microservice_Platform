// -----------------------------------------------------------------------
// <copyright file="EmailAddress.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Client.Models.Emails;

/// <summary>
/// Represents the email address data transfer object.
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
    /// Initializes a new instance of the <see cref="EmailAddress"/> class.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="name">The display name.</param>
    public EmailAddress(string email, string name)
    {
        this.Address = email;
        this.DisplayName = name;
    }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the name associated with the email address.
    /// </summary>
    public string DisplayName { get; set; }
}
