// -----------------------------------------------------------------------
// <copyright file="ApplicationException.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Exceptions;

/// <summary>
/// Represents the base class for application exceptions.
/// </summary>
public class ApplicationException : AppException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationException"/> class with the specified error code and message.
    /// </summary>
    /// <param name="errorCode">The error code that explains the reason for the exception.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ApplicationException(ErrorCode errorCode, string message)
        : base(message)
    {
        this.Code = errorCode.ToString();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationException"/> class with the specified error code.
    /// </summary>
    /// <param name="errorCode">The error code that explains the reason for the exception.</param>
    public ApplicationException(ErrorCode errorCode)
        : base(null)
    {
        this.Code = errorCode.ToString();
    }
}