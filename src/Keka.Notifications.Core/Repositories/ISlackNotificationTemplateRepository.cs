// -----------------------------------------------------------------------
// <copyright file="ISlackNotificationTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents slack notificationtemplate repository.
/// </summary>
public interface ISlackNotificationTemplateRepository
{
    /// <summary>
    /// Gets slack notification template.
    /// </summary>
    /// <param name="templateId">Slack template id.</param>
    /// <returns>Slack template.</returns>
    public Task<SlackNotificationTemplate> GetSlackNotificationTemplateAsync(Guid templateId);

    /// <summary>
    /// Gets template based on template name.
    /// </summary>
    /// <param name="name">Template name.</param>
    /// <returns>Slack Template.</returns>
    Task<SlackNotificationTemplate> GetSlackNotificationTemplateByNameAsync(string name);
}
