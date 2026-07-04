// -----------------------------------------------------------------------
// <copyright file="ApiResponseExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Extensions;

/// <summary>
/// Represents the Api response extension.
/// </summary>
public static class ApiResponseExtensions
{
    /// <summary>
    /// Represents the infinite paged response.
    /// </summary>
    /// <typeparam name="T">source type.</typeparam>
    /// <param name="controller">The controller.</param>
    /// <param name="data">The data.</param>
    /// <param name="continuationToken">The continuation token.</param>
    /// <returns>The paged response.</returns>
    public static IActionResult ToOkInfinitePagedResponse<T>(this ControllerBase controller, IEnumerable<T> data, string continuationToken = null)
    {
        return controller.Ok(new Models.PagedResponse<T>(data, continuationToken));
    }
}
