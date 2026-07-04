// -----------------------------------------------------------------------
// <copyright file="ErrorCode.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Application.Enums;

/// <summary>
/// Represents the exception code.
/// </summary>
public enum ErrorCode
{
    /// <summary>
    /// Indicates invalid email.
    /// </summary>
    INVALID_EMAIL,

    /// <summary>
    /// Indicates unknown errorcode.
    /// </summary>
    UNKNOWN,

    /// <summary>
    /// Indicates invalid or missing arguments.
    /// </summary>
    INVALID_ARGS,

    /// <summary>
    /// Indicates that a specific record was not found.
    /// </summary>
    RECORD_NOT_FOUND,

    /// <summary>
    /// Indicates that a specified service is not supported in the country.
    /// </summary>
    COUNTRY_NOT_SUPPORTED,
}