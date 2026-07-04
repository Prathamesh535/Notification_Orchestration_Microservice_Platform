// -----------------------------------------------------------------------
// <copyright file="IPushNotificationRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// represents the push notification repository interface.
/// </summary>
public interface IPushNotificationRequestRepository
{
    /// <summary>
    /// Gets the push notification request asynchronously.
    /// </summary>
    /// <param name="pushNotificationRequestId">The push notification requestid.</param>
    /// <param name="partitionKey">The partition key.</param>
    /// <returns>Return push notification request.</returns>
    Task<PushNotificationRequest> GetPushNotificationRequestAsync(Guid pushNotificationRequestId, string partitionKey);

    /// <summary>
    /// Asynchronously adds a push notification request.
    /// </summary>
    /// <param name="pushNotificationRequest">The push notification request to insert.</param>
    /// <returns>A task representing the asynchronous operation, containing the GUID of the added notification and partition key.</returns>
    Task<(Guid requestId, string partitionKey)> InsertPushNotificationRequestAsync(PushNotificationRequest pushNotificationRequest);

    /// <summary>
    /// Updates push notification status.
    /// </summary>
    /// <param name="pushNotificationRequest">The push notification that contains status and push notification request id.</param>
    /// <returns>returns the number of rows updated.</returns>
    Task UpdatePushNotificationRequestStatusAsync(PushNotificationRequest pushNotificationRequest);
}
