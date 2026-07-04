// -----------------------------------------------------------------------
// <copyright file="IWaMessageRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents whatsapp message service interface.
/// </summary>
public interface IWaMessageRequestService
{
    /// <summary>
    /// Adds whatsapp message request record.
    /// </summary>
    /// <param name="waMessageRequestDto">WhatsApp message request object.</param>
    /// <returns>The task result which eventually returns identifier of the created record.</returns>
    Task<Guid> AddWaMessageRequestAsync(WaMessageRequestDto waMessageRequestDto);
}