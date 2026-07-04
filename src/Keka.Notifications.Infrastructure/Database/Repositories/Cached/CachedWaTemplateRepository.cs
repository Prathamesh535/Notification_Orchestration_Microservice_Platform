// -----------------------------------------------------------------------
// <copyright file="CachedWaTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Represents the cached wa template repository.
/// </summary>
/// <typeparam name="T">Type of a repository.</typeparam>
internal class CachedWaTemplateRepository<T> : IWaTemplateRepository
    where T : IWaTemplateRepository
{
    private readonly ICacheService cache;
    private readonly T repository;

    public CachedWaTemplateRepository(T repository, ICacheService cache)
    {
        this.repository = repository;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<WaTemplate> GetWaTemplateByIdAsync(Guid waTemplateId)
    {
        var cacheKey = $"WaTemplate:{waTemplateId}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetWaTemplateByIdAsync(waTemplateId), TimeSpan.FromDays(1));
    }

    /// <inheritdoc/>
    public async Task<WaTemplate> GetWaTemplateByNameAsync(string waTemplateName)
    {
        var cacheKey = $"WaTemplate:{waTemplateName}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetWaTemplateByNameAsync(waTemplateName), TimeSpan.FromDays(1));
    }
}