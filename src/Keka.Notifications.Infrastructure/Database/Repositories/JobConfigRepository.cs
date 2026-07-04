// -----------------------------------------------------------------------
// <copyright file="JobConfigRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Class represents the job configuration repository.
/// </summary>
/// <param name="db">The database context.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="appContext">The application context.</param>
/// <remarks>
/// This initializes the job configuration repository.
/// </remarks>
/// <seealso cref="BaseRepository" />
/// <seealso cref="IJobConfigRepository" />
internal class JobConfigRepository(DatabaseContext db, IMapper mapper, IAppContext appContext)
    : BaseRepository(db, mapper, appContext), IJobConfigRepository
{
    /// <inheritdoc/>
    public async Task<IEnumerable<JobConfig>> GetActiveJobConfigsAsync(Guid tenantId)
    {
        var dbJobConfigs = await this.Db.Connection.QueryAsync<DbJobConfig>(JobConfigQuery.SelectJobConfigsByTenant, new { tenantId }, this.Db.Transaction);
        return this.Mapper.Map<IEnumerable<JobConfig>>(dbJobConfigs);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<JobConfig>> GetJobConfigsAsync()
    {
        var dbJobConfigs = await this.Db.Connection.QueryAsync<DbJobConfig>(JobConfigQuery.SelectJobConfigs);
        return this.Mapper.Map<IEnumerable<JobConfig>>(dbJobConfigs);
    }

    /// <inheritdoc/>
    public async Task<JobConfig> GetJobConfigByJobType(Guid tenantId, string jobType)
    {
        var dbJobConfig = await this.Db.Connection.QuerySingleOrDefaultAsync<DbJobConfig>(JobConfigQuery.SelectJobConfigByJobType, new { tenantId, jobType }, this.Db.Transaction);
        return this.Mapper.Map<JobConfig>(dbJobConfig);
    }

    /// <inheritdoc/>
    public async Task<JobConfig> GetJobConfigByIdentifierAsync(Guid jobConfigId)
    {
        var dbJobConfig = await this.Db.Connection.QuerySingleOrDefaultAsync<DbJobConfig>(JobConfigQuery.SelectJobConfigByIdentifier, new { jobConfigId }, this.Db.Transaction);
        return this.Mapper.Map<JobConfig>(dbJobConfig);
    }

    /// <inheritdoc/>
    public async Task<Guid> InsertJobConfigAsync(JobConfig jobConfig)
    {
        var dbJobConfig = this.Mapper.Map<DbJobConfig>(jobConfig);
        dbJobConfig.SetAuditFieldsOnCreate(this.AppContext);
        return await this.Db.Connection.ExecuteScalarAsync<Guid>(JobConfigQuery.InsertJobConfig, dbJobConfig, this.Db.Transaction);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateJobConfigAsync(JobConfig jobConfig)
    {
        var dbJobConfig = this.Mapper.Map<DbJobConfig>(jobConfig);
        dbJobConfig.SetAuditFieldsOnUpdate(this.AppContext);
        return await this.Db.Connection.ExecuteAsync(JobConfigQuery.UpdateJobConfig, dbJobConfig, this.Db.Transaction) > 0;
    }
}
