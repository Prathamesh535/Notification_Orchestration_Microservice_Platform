// -----------------------------------------------------------------------
// <copyright file="TenantQueries.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

/// <summary>
/// Represents the employee query.
/// </summary>
internal static class TenantQueries
{
    /// <summary>
    /// Represents the select employees query.
    /// </summary>
    public const string SelectTenant = @"SELECT * FROM [ns].Tenant";

    /// <summary>
    /// The insert tenant.
    /// </summary>
    public const string InsertTenant = @"INSERT INTO [ns].Tenant (TenantId, Name, CreatedOn, CreatedBy) VALUES (@TenantId, @Name, @CreatedOn, @CreatedBy)";
}
