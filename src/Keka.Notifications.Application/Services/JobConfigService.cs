// -----------------------------------------------------------------------
// <copyright file="JobConfigService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Class represents the job configuration service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JobConfigService"/> class.
/// </remarks>
/// <param name="logger">The logger.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <seealso cref="Keka.Notifications.Application.Services.BaseService" />
/// <seealso cref="IJobConfigService" />
public class JobConfigService(ILogger<JobConfigService> logger, IMapper mapper, IUnitOfWork unitOfWork)
    : BaseService(logger, mapper, unitOfWork), IJobConfigService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<JobConfig>> GetJobConfigsAsync()
    {
        return await this.UnitOfWork.JobConfigRepository.GetJobConfigsAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<JobConfig>> GetJobConfigsAsync(Guid tenantId)
    {
        return await this.UnitOfWork.JobConfigRepository.GetActiveJobConfigsAsync(tenantId);
    }

    /// <inheritdoc/>
    public async Task<JobConfig> GetJobConfigAsync(Guid tenantId, string jobType)
    {
        return await this.UnitOfWork.JobConfigRepository.GetJobConfigByJobType(tenantId, jobType);
    }

    /// <inheritdoc/>
    public async Task<JobConfig> GetJobConfigAsync(Guid jobConfigId)
    {
        return await this.UnitOfWork.JobConfigRepository.GetJobConfigByIdentifierAsync(jobConfigId);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddJobConfigAsync(JobConfig jobConfig)
    {
        return await this.UnitOfWork.JobConfigRepository.InsertJobConfigAsync(jobConfig);
    }

    /// <inheritdoc/>
    public async Task UpdateJobConfigAsync(Guid jobConfigId, JobConfig jobConfig)
    {
        jobConfig.JobConfigId = jobConfigId;
        await this.UnitOfWork.JobConfigRepository.UpdateJobConfigAsync(jobConfig);
    }
}
