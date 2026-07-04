// -----------------------------------------------------------------------
// <copyright file="IPushNotificationRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents the push notification request service interface.
/// </summary>
public interface IPushNotificationRequestService
{
    /// <summary>
    /// Asynchronously inserts a push notification request.
    /// </summary>
    /// <param name="pushNotificationRequestDto">The push notification request to insert.</param>
    /// <returns>A task representing the asynchronous operation, returning an id of inserted record.</returns>
    Task<Guid> AddPushNotificationRequest(PushNotificationRequestDto pushNotificationRequestDto);
}
