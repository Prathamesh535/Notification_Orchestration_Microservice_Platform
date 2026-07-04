// -----------------------------------------------------------------------
// <copyright file="ISmsRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the Interface for the SMS Request repository.
/// </summary>
public interface ISmsRequestRepository
{
    /// <summary>
    /// Asynchronously saves the Sms Request to the Database.
    /// </summary>
    /// <param name="smsRequest">The sms request.</param>
    /// <returns>Returns sms request Identifier and partition key.</returns>
    Task<(Guid requestId, string partitionKey)> SaveSmsRequestAsync(SmsRequest smsRequest);

    /// <summary>
    /// Gets Sms Request based on request Id.
    /// </summary>
    /// <param name="smsRequestId">Sms Request Id.</param>
    /// <param name="partitionKey">Partition Key.</param>
    /// <returns>Task that eventually returns <see cref="SmsRequest"/>.</returns>
    Task<SmsRequest> GetSmsRequestAsync(Guid smsRequestId, string partitionKey);

    /// <summary>
    /// Updates Sms Request status.
    /// </summary>
    /// <param name="smsRequest">Sms Request.</param>
    /// <param name="partitionKey">Partition Key.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateSmsRequestStatusAsync(SmsRequest smsRequest, string partitionKey);
}
