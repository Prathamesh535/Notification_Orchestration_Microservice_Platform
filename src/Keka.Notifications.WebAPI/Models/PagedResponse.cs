// -----------------------------------------------------------------------
// <copyright file="PagedResponse.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Models;

/// <summary>
/// Represents the paged response.
/// </summary>
/// <typeparam name="T">source type.</typeparam>
public class PagedResponse<T> : ApiResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResponse{T}"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="continuationToken">The continuation token.</param>
    public PagedResponse(IEnumerable<T> data, string continuationToken = null)
    {
        this.Data = data;
        this.Success = true;
        this.ContinuationToken = continuationToken;
    }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public IEnumerable<T> Data { get; set; }

    /// <summary>
    /// Gets or sets the continuation token.
    /// </summary>
    public string ContinuationToken { get; set; }
}
