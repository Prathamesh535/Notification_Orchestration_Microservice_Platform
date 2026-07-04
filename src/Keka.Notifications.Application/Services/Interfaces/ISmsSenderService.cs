// -----------------------------------------------------------------------
// <copyright file="ISmsSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents interface of Sms sender service.
/// </summary>
public interface ISmsSenderService
{
    /// <summary>
    /// Sends message via Sms.
    /// </summary>
    /// <param name="sendSmsEvent">Sms Event.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendSmsAsync(SendSmsEvent sendSmsEvent);
}
