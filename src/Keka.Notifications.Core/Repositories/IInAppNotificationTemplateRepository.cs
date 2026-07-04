// -----------------------------------------------------------------------
// <copyright file="IInAppNotificationTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the in app notification template repository.
/// </summary>
public interface IInAppNotificationTemplateRepository
{
    /// <summary>
    /// Gets the in app notification template asynchronously.
    /// </summary>
    /// <param name="inAppNotificationTemplateId">The in app notification template identifier.</param>
    /// <returns>Returns the in app notification template.</returns>
    Task<InAppNotificationTemplate> GetInAppNotificationTemplateByIdAsync(Guid inAppNotificationTemplateId);

    /// <summary>
    /// Gets the in app notification template asynchronously.
    /// </summary>
    /// <param name="inAppNotificationTemplateName">The in app notification template name.</param>
    /// <returns>Returns the in app notification template.</returns>
    Task<InAppNotificationTemplate> GetInAppNotificationTemplateByNameAsync(string inAppNotificationTemplateName);
}