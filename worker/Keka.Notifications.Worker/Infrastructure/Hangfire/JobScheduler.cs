// -----------------------------------------------------------------------
// <copyright file="JobScheduler.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker.Infrastructure.Hangfire;

/// <summary>
/// Represents the job scheduler.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JobScheduler"/> class.
/// </remarks>
/// <param name="recurringJobManager">The recurring job manager.</param>
/// <param name="lifetimeScope">The lifetime scope.</param>
/// <param name="jobConfigService">The job configuration service.</param>
public class JobScheduler(IRecurringJobManager recurringJobManager, ILifetimeScope lifetimeScope, IJobConfigService jobConfigService)
{
    /// <summary>
    /// The recurring job manager.
    /// </summary>
    private readonly IRecurringJobManager recurringJobManager = recurringJobManager;

    /// <summary>
    /// The lifetime scope.
    /// </summary>
    private readonly ILifetimeScope lifetimeScope = lifetimeScope;

    /// <summary>
    /// The job configuration service.
    /// </summary>
    private readonly IJobConfigService jobConfigService = jobConfigService;

    /// <summary>
    /// Gets the tenant job types.
    /// </summary>
    /// <returns>list of system type.</returns>
    public static IEnumerable<Type> GetTenantJobTypes()
    {
        // Get all types in the assembly that implement ITenantJob and are not abstract
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(ITenantJob).IsAssignableFrom(t) && !t.IsAbstract);
    }

    /// <summary>
    /// Schedules the jobs.
    /// </summary>
    /// <returns>Nothing.</returns>
    public async Task ScheduleJobs()
    {
        var jobConfigs = await this.jobConfigService.GetJobConfigsAsync();
        var jobTypes = GetTenantJobTypes();

        foreach (var config in jobConfigs)
        {
            var jobId = $"{config.JobConfigId}";
            var jobType = jobTypes.SingleOrDefault(j => j.Name == config.JobType);
            if (jobType == null || !typeof(ITenantJob).IsAssignableFrom(jobType))
            {
                this.recurringJobManager.RemoveIfExists(jobId);
                continue;
            }

            if (config.IsEnabled)
            {
                var jobAssembly = jobType.AssemblyQualifiedName;
                var jobName = jobType.Name;
                this.recurringJobManager.AddOrUpdate(
                    jobId,
                    () => this.lifetimeScope.Resolve<JobExecutor>().ExecuteAsync(jobAssembly, config.TenantId, jobName, null),
                    config.CronExpression);
            }
            else
            {
                this.recurringJobManager.RemoveIfExists(jobId);
            }
        }
    }
}
