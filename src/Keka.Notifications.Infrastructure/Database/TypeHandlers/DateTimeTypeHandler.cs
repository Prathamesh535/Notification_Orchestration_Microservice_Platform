// -----------------------------------------------------------------------
// <copyright file="DateTimeTypeHandler.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.TypeHandlers;

/// <summary>
/// Class to define the type handler for DateTime.
/// </summary>
internal class DateTimeTypeHandler : SqlMapper.TypeHandler<DateTime>
{
    /// <summary>
    /// Parse the date time value.
    /// </summary>
    /// <param name="value">Date time value.</param>
    /// <returns>Return date time in with time zone info.</returns>
    public override DateTime Parse(object value) => DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);

    /// <summary>
    /// Sets the date time value.
    /// </summary>
    /// <param name="parameter">The db data parameter.</param>
    /// <param name="value">The date time value.</param>
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = value;
    }
}