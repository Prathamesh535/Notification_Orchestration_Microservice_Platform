// -----------------------------------------------------------------------
// <copyright file="UnitOfWorkFactory.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database;

/// <summary>
/// Represents the unit of work factory.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UnitOfWorkFactory"/> class.
/// </remarks>
/// <param name="lifetimeScope">The lifetime scope.</param>
public class UnitOfWorkFactory(ILifetimeScope lifetimeScope)
    : IUnitOfWorkFactory
{
    private readonly ILifetimeScope lifetimeScope = lifetimeScope;

    /// <summary>
    /// Create unit of work instance.
    /// </summary>
    /// <returns>Return the unit of work instance.</returns>
    public IUnitOfWork Create()
    {
        // Resolve UnitOfWork within this scope
        var unitOfWork = this.lifetimeScope.Resolve<IUnitOfWork>();
        return unitOfWork;
    }
}
