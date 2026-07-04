// -----------------------------------------------------------------------
// <copyright file="IUnitOfWorkFactory.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the unit of work factory.
/// </summary>
public interface IUnitOfWorkFactory
{
    /// <summary>
    /// Creates the unit of work instance.
    /// </summary>
    /// <returns>Returns the unit of work instance.</returns>
    IUnitOfWork Create();
}
