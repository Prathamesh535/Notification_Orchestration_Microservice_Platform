// -----------------------------------------------------------------------
// <copyright file="IEmailSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents interface of email sender service.
/// </summary>
public interface IEmailSenderService
{
    /// <summary>
    /// Sends email.
    /// </summary>
    /// <param name="sendEmailEvent">Send email event.</param>
    /// <returns>The task result.</returns>
    Task SendEmailAsync(SendEmailEvent sendEmailEvent);
}
