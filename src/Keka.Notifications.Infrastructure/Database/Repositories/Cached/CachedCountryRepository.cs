// -----------------------------------------------------------------------
// <copyright file="CachedCountryRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Represents the country repository.
/// </summary>
/// <typeparam name="T">Type of a repository.</typeparam>
internal class CachedCountryRepository<T> : ICountryRepository
    where T : ICountryRepository
{
    private readonly ICacheService cache;
    private readonly T repository;

    public CachedCountryRepository(T repository, ICacheService cache)
    {
        this.repository = repository;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<Country> GetCountryByCodeAsync(string countryCode)
    {
        var cacheKey = $"Country:{countryCode}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetCountryByCodeAsync(countryCode), TimeSpan.FromDays(1));
    }
}