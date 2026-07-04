// -----------------------------------------------------------------------
// <copyright file="IPushNotificationTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// represents the push notification template repository interface.
/// </summary>
public interface IPushNotificationTemplateRepository
{
    /// <summary>
    /// Asynchronously retrieves push notification template.
    /// </summary>
    /// <param name="pushNotificationTemplateId">The unique identifier of the push notification template.</param>
    /// <returns>A task representing the asynchronous operation, containing the push notification template.</returns>
    Task<PushNotificationTemplate> GetPushNotificationTemplateByIdAsync(Guid pushNotificationTemplateId);

    /// <summary>
    /// Asynchronously retrieves push notification template.
    /// </summary>
    /// <param name="pushNotificationTemplateName">The name of the push notification template.</param>
    /// <returns>A task representing the asynchronous operation, containing the push notification template.</returns>
    Task<PushNotificationTemplate> GetPushNotificationTemplateByNameAsync(string pushNotificationTemplateName);
}