// -----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents a unit of work for managing database transactions.
/// </summary>
/// <remarks>
/// This interface provides methods for beginning, committing, and rolling back transactions.
/// </remarks>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the repository for managing employees.
    /// </summary>
    IEmployeeRepository EmployeeRepository { get; }

    /// <summary>
    /// Gets the repository for email status.
    /// </summary>
    IEmailStatusRepository EmailStatusRepository { get; }

    /// <summary>
    /// Gets the repository for managing job configurations.
    /// </summary>
    IJobConfigRepository JobConfigRepository { get; }

    /// <summary>
    /// Gets the employee notification prefernce repository.
    /// </summary>
    IEmployeeNotificationPreferenceRepository EmployeeNotificationPreferenceRepository { get; }

    /// <summary>
    /// Gets the country repository.
    /// </summary>
    ICountryRepository CountryRepository { get; }

    /// <summary>
    /// Begins a new transaction.
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    void Commit();

    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    void Rollback();
}
