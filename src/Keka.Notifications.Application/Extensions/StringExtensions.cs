// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Extensions;

/// <summary>
/// Represents a string extension.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Trims the string to the specified maximum length.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <returns>
    /// The trimmed string if the length of the source string exceeds the specified maximum length; otherwise, the original string.
    /// </returns>
    public static string TrimToMaxLength(this string source, int maxLength)
    {
        return source.Length > maxLength ? source.Substring(0, maxLength) : source;
    }
}