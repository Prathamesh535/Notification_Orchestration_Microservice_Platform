// -----------------------------------------------------------------------
// <copyright file="IWaMessageRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents whatsapp message repository interface.
/// </summary>
public interface IWaMessageRepository
{
    /// <summary>
    /// Saves whatsapp message.
    /// </summary>
    /// <param name="waMessage">WhatsApp message.</param>
    /// <returns>Task that eventually returns request identifier.</returns>
    Task<string> SaveWaMessageAsync(WaMessage waMessage);

    /// <summary>
    /// Gets whatsapp message.
    /// </summary>
    /// <param name="waMessageId">WhatsApp message id.</param>
    /// <returns>Task that eventually returns <see cref="WaMessage"/>.</returns>
    Task<WaMessage> GetWaMessageAsync(string waMessageId);

    /// <summary>
    /// Updates whatsapp message.
    /// </summary>
    /// <param name="waMessageId">Whatsapp message id.</param>
    /// <param name="waMessage">Whatsapp message.</param>
    /// <returns>The task.</returns>
    Task UpdateWaMessageAsync(string waMessageId, WaMessage waMessage);
}