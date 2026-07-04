// -----------------------------------------------------------------------
// <copyright file="DomainException.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Exceptions;

/// <summary>
/// Represents the domain exception.
/// </summary>
public abstract class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    protected DomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public virtual string Code { get; }
}
