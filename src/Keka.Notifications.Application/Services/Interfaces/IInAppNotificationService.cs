// -----------------------------------------------------------------------
// <copyright file="IInAppNotificationService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents an in app notification service.
/// </summary>
public interface IInAppNotificationService
{
    /// <summary>
    /// Adds the in app notification request asynchronously.
    /// </summary>
    /// <param name="inAppNotificationRequestDto">The in app notification request dto.</param>
    /// <returns>Returns boolean.</returns>
    Task<string> AddInAppNotificationRequestAsync(InAppNotificationRequestDto inAppNotificationRequestDto);

    /// <summary>
    /// Enriches the in app notification request asynchronously.
    /// </summary>
    /// <param name="inAppNotificationRequestId">The in app notification request identifier.</param>
    /// <param name="partitionKey">The in app notification request partitionKey.</param>
    /// <returns>Returns task.</returns>
    Task EnrichInAppNotificationRequestAsync(string inAppNotificationRequestId, string partitionKey);

    /// <summary>
    /// Marks the in app notification as read.
    /// </summary>
    /// <param name="inAppNotificationId">The in app notification identifier.</param>
    /// <returns>Returns boolean.</returns>
    Task<bool> MarkNotificationAsReadAsync(string inAppNotificationId);

    /// <summary>
    /// Gets the in app notifications.
    /// </summary>
    /// <param name="getInAppNotificationRequest">InAppNotification Request object.</param>
    /// <returns>Returns list of in app notifications dtos.</returns>
    public Task<PagedResponse<InAppNotificationDto>> GetInAppNotifications(GetInAppNotificationRequest getInAppNotificationRequest);

    /// <summary>
    /// Marks all in app notification as read.
    /// </summary>
    /// <returns>Returns boolean.</returns>
    Task<bool> MarkAllNotificationsAsReadAsync();
}