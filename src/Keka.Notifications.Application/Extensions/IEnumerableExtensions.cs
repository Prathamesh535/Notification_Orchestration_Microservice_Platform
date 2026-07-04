// -----------------------------------------------------------------------
// <copyright file="IEnumerableExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Extensions;

/// <summary>
/// This class represents the IEnumerableExtensions.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Splits the list.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <param name="list">The list.</param>
    /// <param name="batchSize">Size of the batch.</param>
    /// <returns>List of list.</returns>
    public static List<List<T>> SplitList<T>(this List<T> list, int batchSize)
    {
        List<List<T>> subLists = new List<List<T>>();
        if (list.Count != 0)
        {
            int totalBatches = (int)Math.Ceiling((double)list.Count / batchSize);
            for (int i = 0; i < totalBatches - 1; i++)
            {
                subLists.Add(list.Skip(i * batchSize).Take(batchSize).ToList());
            }

            subLists.Add(list.Skip((totalBatches - 1) * batchSize).Take(list.Count - ((totalBatches - 1) * batchSize)).ToList());
        }

        return subLists;
    }
}
