// -----------------------------------------------------------------------
// <copyright file="PagedResponse.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models;

/// <summary>
/// Represents the paged response.
/// </summary>
/// <typeparam name="T">source type.</typeparam>
public class PagedResponse<T>
{
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public IEnumerable<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Gets or sets the continuation token.
    /// </summary>
    public string ContinuationToken { get; set; }
}
