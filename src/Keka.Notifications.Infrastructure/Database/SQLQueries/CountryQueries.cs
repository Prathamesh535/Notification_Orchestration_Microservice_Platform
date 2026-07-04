// -----------------------------------------------------------------------
// <copyright file="CountryQueries.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

/// <summary>
/// Represents the Sms queries.
/// </summary>
internal static class CountryQueries
{
    public const string GetCountryByCode = @"
          SELECT 
            [CountryId], 
            [Name], 
            [CountryCode],
            [FromPhoneNumber],
            [IsWaEnabled],
            [IsSmsEnabled]
        FROM 
            [ns].[Country]
        WHERE
            CountryCode = @CountryCode;
    ";
}