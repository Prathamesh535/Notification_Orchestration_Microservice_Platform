// -----------------------------------------------------------------------
// <copyright file="IInAppNotificationRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the in app notification repository.
/// </summary>
public interface IInAppNotificationRepository
{
    /// <summary>
    /// Gets the in app notification.
    /// </summary>
    /// <param name="employeeId">Employee identifier.</param>
    /// <param name="inAppNotificationId">In-appNotification identifier.</param>
    /// <returns>Returns an in app notification.</returns>
    Task<InAppNotification> GetInAppNotification(Guid employeeId, string inAppNotificationId);

    /// <summary>
    /// Gets the in app notifications.
    /// </summary>
    /// <param name="employeeId">Request employee id.</param>
    /// <param name="getInAppNotificationRequest">InAppNotification Request object.</param>
    /// <returns>Returns list of in app notifications.</returns>
    public Task<Core.Models.PagedResponse<InAppNotification>> GetInAppNotifications(Guid employeeId, GetInAppNotificationRequest getInAppNotificationRequest);

    /// <summary>
    /// Adds the in app notification.
    /// </summary>
    /// <param name="inAppNotifications">The list of in app notification.</param>
    /// <returns>Returns list of row key and partition key records.</returns>
    Task<List<(Guid, string)>> AddInAppNotifications(List<InAppNotification> inAppNotifications);

    /// <summary>
    /// Marks the app notification as read.
    /// </summary>
    /// <param name="employeeId">The in user identifier.</param>
    /// <param name="inAppNotificationId">The in app notification identifier.</param>
    /// <returns>Returns boolean.</returns>
    Task<bool> MarkNotificationAsRead(Guid employeeId, string inAppNotificationId);

    /// <summary>
    /// Marks all app notification as read.
    /// </summary>
    /// <param name="employeeId">The in user identifier.</param>
    /// <returns>Returns boolean.</returns>
    Task<bool> MarkAllNotificationsAsRead(Guid employeeId);
}
