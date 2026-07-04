// -----------------------------------------------------------------------
// <copyright file="JobExecutor.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Hangfire.Server;

namespace Keka.Notifications.Worker.Infrastructure.Hangfire;

/// <summary>
/// Represents the job executor.
/// </summary>
/// <param name="lifetimeScope">The lifetime scope.</param>
/// <param name="logger">The logger.</param>
public class JobExecutor(ILifetimeScope lifetimeScope, ILogger<JobExecutor> logger)
{
    private readonly ILifetimeScope lifetimeScope = lifetimeScope;

    /// <summary>
    /// Method Execute Async.
    /// Here Job Display will be the job name.
    /// {2} is the job name.
    /// </summary>
    /// <param name="jobTypeName">The jobType Name.</param>
    /// <param name="tenantId">The tenant Id.</param>
    /// <param name="jobName">The job Name.</param>
    /// <param name="context">The perform context.</param>
    /// <returns>Task.</returns>
    [JobDisplayName("{2}")]
    public Task ExecuteAsync(string jobTypeName, Guid tenantId, string jobName, PerformContext context)
    {
        context.SetJobParameter("TenantId", tenantId);
        return Task.Run(async () =>
        {
            try
            {
                var isJobNotFounded = true;
                await using var scope = this.lifetimeScope.BeginLifetimeScope();
                var jobType = Type.GetType(jobTypeName);
                if (jobType != null)
                {
                    this.SetAppContext(context);
                    var tenantJob = scope.Resolve(jobType) as ITenantJob;
                    if (tenantJob != null)
                    {
                        logger.LogTrace("Job: {jobName} is started executing.", jobName);
                        await tenantJob.ExecuteAsync(tenantId);
                        logger.LogTrace("Job: {jobName} is successfully executed.", jobName);
                        isJobNotFounded = false;
                    }
                }

                if (isJobNotFounded)
                {
                    logger.LogError("Job: {jobName} job type not found.", jobName);
                }
            }
            catch (Exception)
            {
                logger.LogCritical("Error occurred while executing job: {jobName}", jobName);
                throw;
            }
        });
    }

    /// <summary>
    /// Set AppContext.
    /// </summary>
    /// <param name="context">The Perform context.</param>
    private void SetAppContext(PerformContext context)
    {
        var jobId = context.GetJobParameter<Guid>("RecurringJobId");
        var tenantId = context.GetJobParameter<Guid>("TenantId");

        var appcontextAccessor = this.lifetimeScope.Resolve<IAppContextAccessor>();
        appcontextAccessor.CurrentAppContext = new AppRequestContext(tenantId, jobId, AppContextType.System);
    }
}
