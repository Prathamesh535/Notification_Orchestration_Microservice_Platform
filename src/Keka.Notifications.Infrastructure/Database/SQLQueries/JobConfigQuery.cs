// -----------------------------------------------------------------------
// <copyright file="JobConfigQuery.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

/// <summary>
/// Class represents the queries for the job config.
/// </summary>
internal static class JobConfigQuery
{
    /// <summary>
    /// The select job configs by tenant.
    /// </summary>
    public const string SelectJobConfigsByTenant = @"
        SELECT
            JobConfigId,
            TenantId,
            JobType,
            CronExpression,
            IsEnabled
        FROM
            [ns].[JobConfig]
        WHERE
            TenantId = @TenantId";

    /// <summary>
    /// The select job configs.
    /// </summary>
    public const string SelectJobConfigs = @"
        SELECT
            JobConfigId,
            TenantId,
            JobType,
            CronExpression,
            IsEnabled
        FROM
            [ns].[JobConfig]";

    /// <summary>
    /// The select job configuration by job type.
    /// </summary>
    public const string SelectJobConfigByJobType = @"SELECT * FROM [ns].[JobConfig] WHERE TenantId = @TenantId AND JobType = @JobType";

    /// <summary>
    /// The select job configuration by identifier.
    /// </summary>
    public const string SelectJobConfigByIdentifier = @"SELECT * FROM [ns].[JobConfig] WHERE JobConfigId = @JobConfigId";

    /// <summary>
    /// The insert job configuration.
    /// </summary>
    public const string InsertJobConfig = @"
        INSERT INTO [ns].[JobConfig] (JobConfigId, TenantId, JobType, CronExpression, IsEnabled, CreatedOn, CreatedBy)
        OUTPUT INSERTED.JobConfigId
        VALUES(DEFAULT, @TenantId, @JobType, @CronExpression, @IsEnabled, @CreatedOn, @CreatedBy)";

    /// <summary>
    /// The update job configuration.
    /// </summary>
    public const string UpdateJobConfig = @"UPDATE [ns].[JobConfig] SET IsEnabled = @IsEnabled, CronExpression = @CronExpression, UpdatedOn = @UpdatedOn, UpdatedBy = @UpdatedBy WHERE JobConfigId = @JobConfigId";
}
