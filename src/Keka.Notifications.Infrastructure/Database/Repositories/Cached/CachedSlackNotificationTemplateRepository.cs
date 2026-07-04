// -----------------------------------------------------------------------
// <copyright file="CachedSlackNotificationTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.Cached;

/// <summary>
/// Represents the cached slack notification template repository.
/// </summary>
/// <typeparam name="T">Type of repository.</typeparam>
internal class CachedSlackNotificationTemplateRepository<T> : ISlackNotificationTemplateRepository
where T : ISlackNotificationTemplateRepository
{
    private readonly ICacheService cache;
    private readonly T repository;

    public CachedSlackNotificationTemplateRepository(T repository, ICacheService cache)
    {
        this.repository = repository;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<SlackNotificationTemplate> GetSlackNotificationTemplateAsync(Guid templateId)
    {
        var cacheKey = $"SlackTemplate:{templateId}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetSlackNotificationTemplateAsync(templateId), TimeSpan.FromDays(1));
    }

    /// <inheritdoc/>
    public async Task<SlackNotificationTemplate> GetSlackNotificationTemplateByNameAsync(string name)
    {
        var cacheKey = $"SlackTemplate:{name}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetSlackNotificationTemplateByNameAsync(name), TimeSpan.FromDays(1));
    }
}
