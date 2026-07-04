// -----------------------------------------------------------------------
// <copyright file="CachedSmsTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.Cached;

/// <summary>
/// Represents the cached sms template repository.
/// </summary>
/// <typeparam name="T">Type of a repository.</typeparam>
internal class CachedSmsTemplateRepository<T> : ISmsTemplateRepository
    where T : ISmsTemplateRepository
{
    private readonly ICacheService cache;
    private readonly T repository;

    public CachedSmsTemplateRepository(T repository, ICacheService cache)
    {
        this.repository = repository;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<SmsTemplate> GetSmsTemplateByIdAsync(Guid smsTemplateId)
    {
        var cacheKey = $"SmsTempalte:{smsTemplateId}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetSmsTemplateByIdAsync(smsTemplateId), TimeSpan.FromDays(1));
    }

    /// <inheritdoc/>
    public async Task<SmsTemplate> GetSmsTemplateByNameAsync(string smsTemplateName)
    {
        var cacheKey = $"SmsTempalte:{smsTemplateName}";
        return await this.cache.GetOrSetAsync(cacheKey, () => this.repository.GetSmsTemplateByNameAsync(smsTemplateName), TimeSpan.FromDays(1));
    }
}