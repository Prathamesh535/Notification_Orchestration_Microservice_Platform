// -----------------------------------------------------------------------
// <copyright file="IEmailService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents an email service.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Add email request asynchronously.
    /// </summary>
    /// <param name="emailRequestDto">The email message to send.</param>
    /// <returns>Returns Identifier of email request.</returns>
    Task<Guid> AddEmailRequestAsync(EmailRequestDto emailRequestDto);

    /// <summary>
    /// Enriches the email request asynchronously and schedule email messages for delivery.
    /// </summary>
    /// <param name="emailRequestId">The email request id.</param>
    /// <returns>The task result.</returns>
    Task EnrichEmailRequestAsync(string emailRequestId);
}