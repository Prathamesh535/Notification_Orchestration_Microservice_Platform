// -----------------------------------------------------------------------
// <copyright file="EmployeeQueries.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

/// <summary>
/// Represents the employee query.
/// </summary>
internal static class EmployeeQueries
{
    /// <summary>
    /// Represents the select employees query.
    /// </summary>
    public const string SelectEmployees = @"SELECT EmployeeId, TenantId FROM [ns].Employee Where EmployeeId = @EmployeeId";

    /// <summary>
    /// The insert employee.
    /// </summary>
    public const string InsertEmployee = @"INSERT INTO [ns].Employee (EmployeeId, TenantId, CreatedOn, CreatedBy) VALUES (@EmployeeId, @TenantId, @CreatedOn, @CreatedBy)";
}
