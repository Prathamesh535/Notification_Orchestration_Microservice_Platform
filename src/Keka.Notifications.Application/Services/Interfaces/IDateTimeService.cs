// -----------------------------------------------------------------------
// <copyright file="IDateTimeService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents datetime service.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Gets current utc time.
    /// </summary>
    /// <returns>Datetime object.</returns>
    DateTime GetCurrentTimeUtc();

    /// <summary>
    /// Gets current inverted ticks.
    /// </summary>
    /// <returns>Datetime object.</returns>
    string GetInvertedTicks();

    /// <summary>
    /// Gets Date in YYYYMM format.
    /// </summary>
    /// <returns>Date object as string.</returns>
    string GetCurrentYearMonth();
}