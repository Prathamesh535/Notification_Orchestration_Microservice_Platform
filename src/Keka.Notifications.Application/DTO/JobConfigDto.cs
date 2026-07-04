// -----------------------------------------------------------------------
// <copyright file="JobConfigDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO;

/// <summary>
/// Represents the job configuration.
/// </summary>
public class JobConfigDto
{
    /// <summary>
    /// Gets or sets the job identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the job type.
    /// </summary>
    public string JobType { get; set; }

    /// <summary>
    /// Gets or sets the cron expression for job execution.
    /// </summary>
    public string CronExpression { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the job is enabled or not.
    /// </summary>
    public bool IsEnabled { get; set; }
}
