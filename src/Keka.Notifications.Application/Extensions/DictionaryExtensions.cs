// -----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Extensions;

/// <summary>
/// Represents a dictionary Extension.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// Gets the hash code of a dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to get the hash code for.</param>
    /// <returns>The hash code of the dictionary.</returns>
    public static int GetDataHashCode<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
    {
        var sortedPairs = dictionary.OrderBy(kvp => kvp.Key);
        int hash = 17;
        foreach (var kvp in sortedPairs)
        {
            int keyHashCode = kvp.Key.GetHashCode();
            int valueHashCode = kvp.Value?.GetHashCode() ?? 0;
            hash = (hash * 31) + keyHashCode;
            hash = (hash * 31) + valueHashCode;
        }

        return hash;
    }

    /// <summary>
    /// Determines whether the dictionary contains all the specified keys.
    /// </summary>
    /// <param name="dict">The dictionary to check for the presence of keys.</param>
    /// <param name="keys">A list of keys to check for in the dictionary.</param>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <returns>Returns boolean value after checking wheather keys are present in the dictionary.</returns>
    public static bool HasKeys<TKey, TValue>(this Dictionary<TKey, TValue> dict, List<TKey> keys)
    {
        if (keys is not null)
        {
            foreach (TKey key in keys)
            {
                if (!dict.ContainsKey(key))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
