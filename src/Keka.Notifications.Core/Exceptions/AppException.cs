// -----------------------------------------------------------------------
// <copyright file="AppException.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Exceptions;

/// <summary>
/// Represents the base class for application exceptions.
/// </summary>
public abstract class AppException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    protected AppException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Gets or Sets the error code associated with the exception.
    /// </summary>
    public virtual string Code { get; protected set; }
}
