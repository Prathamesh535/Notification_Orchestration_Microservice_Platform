// -----------------------------------------------------------------------
// <copyright file="DateTimeService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Services;

/// <summary>
/// Represents date time service.
/// </summary>
internal class DateTimeService : IDateTimeService
{
    /// <inheritdoc/>
    public DateTime GetCurrentTimeUtc()
    {
        return DateTime.UtcNow;
    }

    /// <inheritdoc/>
    public string GetInvertedTicks()
    {
        return string.Format("{0:D19}", DateTime.MaxValue.Ticks - this.GetCurrentTimeUtc().Ticks);
    }

    /// <inheritdoc/>
    public string GetCurrentYearMonth()
    {
        return this.GetCurrentTimeUtc().ToString("yyyyMM");
    }
}