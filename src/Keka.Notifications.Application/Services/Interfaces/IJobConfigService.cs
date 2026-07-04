// -----------------------------------------------------------------------
// <copyright file="IJobConfigService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Interface for job config service.
/// </summary>
public interface IJobConfigService
{
    /// <summary>
    /// Gets the job configs asynchronous.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <returns>list of job config dto's.</returns>
    Task<IEnumerable<JobConfig>> GetJobConfigsAsync(Guid tenantId);

    /// <summary>
    /// Gets the job configs asynchronous.
    /// </summary>
    /// <returns>list of job config dto's.</returns>
    Task<IEnumerable<JobConfig>> GetJobConfigsAsync();

    /// <summary>
    /// Gets the job configuration asynchronous.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="jobType">Type of the job.</param>
    /// <returns>Job configuration.</returns>
    Task<JobConfig> GetJobConfigAsync(Guid tenantId, string jobType);

    /// <summary>
    /// Gets the job configuration by identifier asynchronous.
    /// </summary>
    /// <param name="jobConfigId">The job config identifier.</param>
    /// <returns>job configuration.</returns>
    Task<JobConfig> GetJobConfigAsync(Guid jobConfigId);

    /// <summary>
    /// Adds the job configuration.
    /// </summary>
    /// <param name="jobConfig">The job configuration.</param>
    /// <returns>JobConfigId of the inserted config.</returns>
    Task<Guid> AddJobConfigAsync(JobConfig jobConfig);

    /// <summary>
    /// Toggles the job configuration.
    /// </summary>
    /// <param name="jobConfigId">The job config identifier.</param>
    /// <param name="jobConfig">The job configuration.</param>
    /// <returns>Return asynchronous task result.</returns>
    Task UpdateJobConfigAsync(Guid jobConfigId, JobConfig jobConfig);
}
