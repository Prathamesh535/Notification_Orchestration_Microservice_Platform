// -----------------------------------------------------------------------
// <copyright file="DateOnlyTypeHandler.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.TypeHandlers;

/// <summary>
/// Represents the date only type handler.
/// </summary>
internal class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    /// <summary>
    /// Parse the date only value.
    /// </summary>
    /// <param name="value">The date only value.</param>
    /// <returns>Return the date only.</returns>
    public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);

    /// <summary>
    /// Sets the date only value.
    /// </summary>
    /// <param name="parameter">The db data parameter.</param>
    /// <param name="value">The date only.</param>
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.Date;
        parameter.Value = value;
    }
}
