// -----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database;

/// <summary>
/// Represents the unit of work.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ILifetimeScope lifetimeScope;
    private readonly DatabaseContext dbContext;
    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="dbContext">The database connection.</param>
    /// <param name="lifetimeScope">The autofac lifetime scope.</param>
    public UnitOfWork(DatabaseContext dbContext, ILifetimeScope lifetimeScope)
    {
        this.dbContext = dbContext;
        this.lifetimeScope = lifetimeScope;
    }

    /// <inheritdoc />
    public IEmployeeRepository EmployeeRepository => this.lifetimeScope.Resolve<IEmployeeRepository>();

    /// <inheritdoc />
    public IEmailStatusRepository EmailStatusRepository => this.lifetimeScope.Resolve<IEmailStatusRepository>();

    /// <inheritdoc />
    public IJobConfigRepository JobConfigRepository => this.lifetimeScope.Resolve<IJobConfigRepository>();

    /// <inheritdoc />
    public IEmployeeNotificationPreferenceRepository EmployeeNotificationPreferenceRepository => this.lifetimeScope.Resolve<IEmployeeNotificationPreferenceRepository>();

    /// <inheritdoc />
    public ICountryRepository CountryRepository => this.lifetimeScope.Resolve<ICountryRepository>();

    /// <inheritdoc />
    public void BeginTransaction()
    {
        this.dbContext.BeginTransaction();
    }

    /// <inheritdoc />
    public void Commit()
    {
        this.dbContext.Transaction?.Commit();
        this.Dispose();
    }

    /// <inheritdoc />
    public void Rollback()
    {
        this.dbContext.Transaction?.Rollback();
        this.Dispose();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
    /// <param name="disposing">The instance is disposing.</param>
    private void Dispose(bool disposing)
    {
        if (this.disposed)
        {
            return;
        }

        if (disposing)
        {
            // Dispose managed state (managed objects)
            this.dbContext?.Dispose();
        }

        this.disposed = true;
    }
}
