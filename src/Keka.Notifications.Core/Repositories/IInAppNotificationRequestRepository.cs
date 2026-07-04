// -----------------------------------------------------------------------
// <copyright file="IInAppNotificationRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents an in app notification request repository.
/// </summary>
public interface IInAppNotificationRequestRepository
{
    /// <summary>
    /// Saves the in app notification request asynchronously.
    /// </summary>
    /// <param name="inAppNotificationRequest">The in app notification request.</param>
    /// <returns>returns guid.</returns>
    Task<(string requestId, string partitionKey)> SaveInAppNotificationRequestAsync(InAppNotificationRequest inAppNotificationRequest);

    /// <summary>
    /// Gets the notification request asynchronously.
    /// </summary>
    /// <param name="inAppNotificationRequestId">The in app notification request identifier.</param>
    /// <param name="partitionKey">The in app notification request partitionkey.</param>
    /// <returns>Returns in app notification request.</returns>
    Task<InAppNotificationRequest> GetInAppNotificationRequestAsync(string inAppNotificationRequestId, string partitionKey);

    /// <summary>
    /// Updates the in app notification request status asynchronous.
    /// </summary>
    /// <param name="inAppNotificationRequest">The in application notification request.</param>
    /// <param name="partitionKey">The Partition Key of In Application Request.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateInAppNotificationRequestStatusAsync(InAppNotificationRequest inAppNotificationRequest, string partitionKey);
}