// -----------------------------------------------------------------------
// <copyright file="DatabaseContext.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database;

/// <summary>
/// Provides functionalities to establish and configure database connections using Dapper.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DatabaseContext" /> class.
/// </remarks>
public sealed class DatabaseContext : IDisposable
{
    private readonly string connectionString;
    private readonly IAppContext appContext;
    private bool disposed;
    private IDbConnection connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
    /// </summary>
    /// <param name="connectionString">The sql connection string.</param>
    /// <param name="appContext">The app context.</param>
    public DatabaseContext(string connectionString, IAppContext appContext)
    {
        this.connectionString = connectionString;
        this.appContext = appContext;
    }

    /// <summary>
    /// Gets the database connection, creating it if it doesn't exist.
    /// </summary>
    public IDbConnection Connection
    {
        get
        {
            this.EnsureConnectionOpen();
            return this.connection;
        }
    }

    /// <summary>
    /// Gets the database transaction.
    /// </summary>
    public IDbTransaction Transaction { get; private set; }

    /// <summary>
    /// Begins the database transaction.
    /// </summary>
    public void BeginTransaction()
    {
        this.Transaction = this.Connection.BeginTransaction();
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void EnsureConnectionOpen()
    {
        // If connection is available and open, return. Else create new connection and set session context.
        if (this.connection is { State: ConnectionState.Open })
        {
            return;
        }

        this.connection = new SqlConnection(this.connectionString);
        this.connection.Open();
        this.SetSessionContext();
    }

    /// <summary>
    /// Set session context when the connection is open.
    /// </summary>
    private void SetSessionContext()
    {
        // If tenant id is not available, return.
        if (this.appContext.TenantId == Guid.Empty)
        {
            return;
        }

        // SQL to set the session context; adjust the key/value as per your context
        const string cmdText = "EXEC sp_set_session_context @key=N'TenantId', @value=@tenantId";
        this.Connection.Execute(cmdText, new { tenantId = this.appContext.TenantId });
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
            this.connection?.Dispose();
            this.Transaction?.Dispose();
        }

        this.disposed = true;
    }
}
