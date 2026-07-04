// -----------------------------------------------------------------------
// <copyright file="EmployeeNotificationPreferenceQueries.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

/// <summary>
/// Contains SQL queries related to employee notification preferences.
/// </summary>
internal static class EmployeeNotificationPreferenceQueries
{
    /// <summary>
    /// SQL query to insert a new employee notification preference.
    /// </summary>
    public const string InsertEmployeeNotificationPreference = @"
            INSERT INTO [ns].[EmployeeNotificationPreference]
                ([TenantId],
                [EmployeeId],
                [IsEnabled],
                [EventId],
                [CreatedOn],
                [CreatedBy])
            VALUES
                (@TenantId,
                @EmployeeId,
                @IsEnabled,
                @EventId,
                @CreatedOn,
                @CreatedBy)";

    /// <summary>
    /// SQL query to delete an existing employee notification preference.
    /// </summary>
    public const string DeleteEmployeeNotificationPreferences = @"
            DELETE FROM
                [ns].[EmployeeNotificationPreference]
            WHERE
                EmployeeId = @EmployeeId AND EventId = @EventId";

    /// <summary>
    /// SQL query to retrieve employee notification preferences for all events.
    /// </summary>
    public const string GetEmployeeNotificationPreferences = @"
            SELECT
                m.[Name] AS ModuleName,
                e.[Name] AS EventName,
                e.[Description] as EventDescription,
                e.[EventId],
                e.[EventCode] as EventCode,
                e.[NotificationChannels] as NotificationChannels,
                CASE WHEN enp.EventId IS NOT NULL THEN 0 ELSE 1 END AS IsEnabled
            FROM
                [ns].[Event] AS e
                INNER JOIN [ns].[Module] AS m ON e.ModuleId = m.ModuleId
                LEFT JOIN [ns].[EmployeeNotificationPreference] AS enp ON enp.[EmployeeId] = @EmployeeId AND enp.EventId = e.EventId";

    /// <summary>
    /// SQL query to retrieve disabled employee notification preferences.
    /// </summary>
    public const string GetDisabledEmployeeNotificationPreferences = @"
            SELECT
                [EventId],
                [EmployeeId]
            FROM
                [ns].[EmployeeNotificationPreference]
            WHERE
                EmployeeId = @EmployeeId";
}