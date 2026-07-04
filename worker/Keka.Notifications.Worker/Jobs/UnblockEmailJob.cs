// -----------------------------------------------------------------------
// <copyright file="UnblockEmailJob.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker.Jobs;

/// <summary>
/// Represents unblock email job.
/// </summary>
public class UnblockEmailJob : ITenantJob
{
    private readonly ILogger<UnblockEmailJob> logger;
    private readonly IEmailStatusService emailStatusService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnblockEmailJob"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="emailStatusService">The email status service instance.</param>
    public UnblockEmailJob(ILogger<UnblockEmailJob> logger, IEmailStatusService emailStatusService)
    {
        this.logger = logger;
        this.emailStatusService = emailStatusService;
    }

    /// <inheritdoc/>
    public async Task ExecuteAsync(Guid tenantId)
    {
        this.logger.LogInformation("UnblockEmailJob has started.");
        try
        {
            int noOfUsersUnblocked = await this.emailStatusService.UnblockEmailsAsync();
            this.logger.LogInformation("No of users unblocked : {noOfUsersUnblocked}", noOfUsersUnblocked);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error while executing UnblockEmailJob.");
        }
        finally
        {
            this.logger.LogInformation("UnblockEmailJob has completed.");
        }
    }
}
