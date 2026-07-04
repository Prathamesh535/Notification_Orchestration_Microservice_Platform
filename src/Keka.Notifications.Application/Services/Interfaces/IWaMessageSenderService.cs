// -----------------------------------------------------------------------
// <copyright file="IWaMessageSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents interface of whatsapp sender service.
/// </summary>
public interface IWaMessageSenderService
{
    /// <summary>
    /// Sends message via whatsapp.
    /// </summary>
    /// <param name="sendWaMessageEvent">Model representing the details of the WhatsApp message to send.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
    Task SendWaMessageAsync(SendWaMessageEvent sendWaMessageEvent);
}
