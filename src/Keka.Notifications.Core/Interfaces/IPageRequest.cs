// -----------------------------------------------------------------------
// <copyright file="IPageRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Interfaces;

/// <summary>
/// Represents paged request interface.
/// </summary>
public interface IPageRequest
{
    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Gets or sets the continuation token.
    /// </summary>
    public string ContinuationToken { get; set; }
}
