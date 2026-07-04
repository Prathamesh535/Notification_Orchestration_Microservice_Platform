// -----------------------------------------------------------------------
// <copyright file="CachedPushNotificationTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Represents the cached push notification request template repository.
/// </summary>
/// <typeparam name="T">Type of a repository.</typeparam>
/// <seealso cref="Keka.Notifications.Core.Repositories.IPushNotificationTemplateRepository" />
internal class CachedPushNotificationTemplateRepository<T> : IPushNotificationTemplateRepository
    where T : IPushNotificationTemplateRepository
{
    private readonly ICacheService cache;
    private readonly T repository;

    public CachedPushNotificationTemplateRepository(T repository, ICacheService cache)
    {
        this.repository = repository;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<PushNotificationTemplate> GetPushNotificationTemplateByIdAsync(Guid pushNotificationTemplateId)
    {
        var cacheKey = $"PushNotificationTemplate:{pushNotificationTemplateId}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetPushNotificationTemplateByIdAsync(pushNotificationTemplateId), TimeSpan.FromDays(1));
    }

    /// <inheritdoc/>
    public async Task<PushNotificationTemplate> GetPushNotificationTemplateByNameAsync(string pushNotificationTemplateName)
    {
        var cacheKey = $"PushNotificationTemplate:{pushNotificationTemplateName}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetPushNotificationTemplateByNameAsync(pushNotificationTemplateName), TimeSpan.FromDays(1));
    }
}
