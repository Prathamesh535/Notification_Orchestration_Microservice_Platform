// -----------------------------------------------------------------------
// <copyright file="IJobConfigRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Interface implements the job config repository.
/// </summary>
public interface IJobConfigRepository
{
    /// <summary>
    /// Gets the active job configs asynchronous.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <returns>The job configuration.</returns>
    Task<IEnumerable<JobConfig>> GetActiveJobConfigsAsync(Guid tenantId);

    /// <summary>
    /// Gets the job configs asynchronous.
    /// </summary>
    /// <returns>The job configuration.</returns>
    Task<IEnumerable<JobConfig>> GetJobConfigsAsync();

    /// <summary>
    /// Updates the job configuration asynchronous.
    /// </summary>
    /// <param name="jobConfig">The job configuration.</param>
    /// <returns>true if updated successfully.</returns>
    Task<bool> UpdateJobConfigAsync(JobConfig jobConfig);

    /// <summary>
    /// Inserts the job configuration asyc.
    /// </summary>
    /// <param name="jobConfig">The job configuration.</param>
    /// <returns>JobConfigId of the inserted job configuration.</returns>
    Task<Guid> InsertJobConfigAsync(JobConfig jobConfig);

    /// <summary>
    /// Gets the type of the job configuration by job.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="jobType">Type of the job.</param>
    /// <returns>Job Configuration of that job type.</returns>
    Task<JobConfig> GetJobConfigByJobType(Guid tenantId, string jobType);

    /// <summary>
    /// Gets the job configuration by identifier asynchronous.
    /// </summary>
    /// <param name="jobConfigId">The job config identifier.</param>
    /// <returns>Job configuration of that identifier.</returns>
    Task<JobConfig> GetJobConfigByIdentifierAsync(Guid jobConfigId);
}
