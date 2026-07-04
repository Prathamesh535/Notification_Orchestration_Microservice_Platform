// -----------------------------------------------------------------------
// <copyright file="CachedInAppNotificationTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Represents the cached in app notification template repository.
/// </summary>
/// <typeparam name="T">Type of a repository.</typeparam>
/// <seealso cref="Keka.Notifications.Core.Repositories.IInAppNotificationTemplateRepository" />
internal class CachedInAppNotificationTemplateRepository<T> : IInAppNotificationTemplateRepository
    where T : IInAppNotificationTemplateRepository
{
    private readonly ICacheService cache;
    private readonly T repository;

    public CachedInAppNotificationTemplateRepository(T repository, ICacheService cache)
    {
        this.repository = repository;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<InAppNotificationTemplate> GetInAppNotificationTemplateByIdAsync(Guid inAppNotificationTemplateId)
    {
        var cacheKey = $"InAppNotificationTemplate:{inAppNotificationTemplateId}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetInAppNotificationTemplateByIdAsync(inAppNotificationTemplateId), TimeSpan.FromDays(1));
    }

    /// <inheritdoc/>
    public async Task<InAppNotificationTemplate> GetInAppNotificationTemplateByNameAsync(string inAppNotificationTemplateName)
    {
        var cacheKey = $"InAppNotificationTemplate:{inAppNotificationTemplateName}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetInAppNotificationTemplateByNameAsync(inAppNotificationTemplateName), TimeSpan.FromDays(1));
    }
}