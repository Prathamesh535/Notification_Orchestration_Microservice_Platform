// -----------------------------------------------------------------------
// <copyright file="ISmsRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents a Interface for Sms Request Service.
/// </summary>
public interface ISmsRequestService
{
    /// <summary>
    /// Add Sms Request Asynchronously.
    /// </summary>
    /// <param name="smsRequestDto">The Sms Request.</param>
    /// <returns>The Task result contains the unique identifier.</returns>
    Task<Guid> AddSmsRequestAsync(SmsRequestDto smsRequestDto);
}
