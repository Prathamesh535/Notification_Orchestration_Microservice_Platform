// -----------------------------------------------------------------------
// <copyright file="IEmailRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents an email repository.
/// </summary>
public interface IEmailRequestRepository
{
    /// <summary>
    /// Saves the email request asynchronous.
    /// </summary>
    /// <param name="emailRequest">The email request.</param>
    /// <returns>Returns email request identifier.</returns>
    Task<string> SaveEmailRequestAsync(EmailRequest emailRequest);

    /// <summary>
    /// Gets the email request asynchronous.
    /// </summary>
    /// <param name="emailRequestId">The email request identifier.</param>
    /// <returns>Returns email request.</returns>
    Task<EmailRequest> GetEmailRequestAsync(string emailRequestId);

    /// <summary>
    /// Deletes the email request record.
    /// </summary>
    /// <param name="emailRequestId">The ID of the email request to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteEmailRequestAsync(string emailRequestId);
}