// -----------------------------------------------------------------------
// <copyright file="JobConfig.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Jobs;

/// <summary>
/// Represents the job configuration.
/// </summary>
public class JobConfig
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public Guid JobConfigId { get; set; }

    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    /// <value>
    /// The tenant identifier.
    /// </value>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the type of the job.
    /// </summary>
    /// <value>
    /// The type of the job.
    /// </value>
    public string JobType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cron expression.
    /// </summary>
    /// <value>
    /// The cron expression.
    /// </value>
    public string CronExpression { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool IsEnabled { get; set; }
}
