// -----------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Extensions;

/// <summary>
/// Represent a Enum Extension.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets Description of enum.
    /// </summary>
    /// <typeparam name="T">source type.</typeparam>
    /// <param name="enumValue">The source.</param>
    /// <returns>
    /// The description.
    /// </returns>
    public static string GetDescription<T>(this T enumValue)
        where T : Enum
    {
        var type = enumValue.GetType();

        var memberInfo = type.GetField(enumValue.ToString());

        if (memberInfo is null)
        {
            return null;
        }

        var descriptionAttribute = memberInfo
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault();

        return descriptionAttribute?.Description;
    }
}
