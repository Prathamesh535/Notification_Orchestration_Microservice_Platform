// -----------------------------------------------------------------------
// <copyright file="JobConfigQueries.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

/// <summary>
/// Represents the job config query.
/// </summary>
internal static class JobConfigQueries
{
    /// <summary>
    /// Selects the job configs by tenant.
    /// </summary>
    public const string SelectJobConfigsByTenant = @"
        SELECT 
            Id, TenantId, JobType, CronExpression, IsEnabled
        FROM 
            JobConfig 
        WHERE 
            TenantId = @TenantId";

    /// <summary>
    /// Selects the job configs across all tenants.
    /// </summary>
    public const string SelectJobConfigs = @"
        SELECT 
            Id, TenantId, JobType, CronExpression, IsEnabled
        FROM 
            JobConfig";
}
