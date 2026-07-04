// -----------------------------------------------------------------------
// <copyright file="HttpMethodType.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Enums;

/// <summary>
/// Represents the enums for http request type.
/// </summary>
public enum HttpMethodType : short
{
    /// <summary>
    /// Indicates none.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates http request type as get.
    /// </summary>
    GET = 1,

    /// <summary>
    /// Indicates http request type as post.
    /// </summary>
    POST = 2,

    /// <summary>
    /// Indicates http request type as put.
    /// </summary>
    PUT = 3,

    /// <summary>
    /// Indicates http request type as patch.
    /// </summary>
    PATCH = 4,

    /// <summary>
    /// Indicates http request type as delete.
    /// </summary>
    DELETE = 5,
}