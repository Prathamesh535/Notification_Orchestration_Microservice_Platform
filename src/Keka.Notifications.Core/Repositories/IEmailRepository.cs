// -----------------------------------------------------------------------
// <copyright file="IEmailRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the email message repository.
/// </summary>
public interface IEmailRepository
{
    /// <summary>
    /// Saves an email message.
    /// </summary>
    /// <param name="email">The email message.</param>
    /// <returns>Return the message identifier.</returns>
    Task<string> SaveEmailAsync(Email email);

    /// <summary>
    /// Gets the email message asynchronously.
    /// </summary>
    /// <param name="emailId">The email id.</param>
    /// <returns>Return email message.</returns>
    Task<Email> GetEmailAsync(string emailId);

    /// <summary>
    /// Deletes the email record.
    /// </summary>
    /// <param name="emailId">The ID of the email to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteEmailAsync(string emailId);
}
