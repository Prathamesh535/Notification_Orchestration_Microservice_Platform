// -----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Represents a base repository that provides common functionalities and properties
/// to be inherited by other specific repositories.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BaseRepository"/> class.
/// </remarks>
/// <param name="db">The Dapper context, providing functionalities for database interactions.</param>
/// <param name="mapper">The AutoMapper instance, used to map between domain models and data entities.</param>
/// <param name="appContext">The app context.</param>
internal class BaseRepository(DatabaseContext db, IMapper mapper, IAppContext appContext)
{
    /// <summary>
    /// Provides functionalities to establish and configure database connections using Dapper.
    /// </summary>
    protected readonly DatabaseContext Db = db;

    /// <summary>
    /// Provides mapping between domain models and data entities.
    /// </summary>
    protected readonly IMapper Mapper = mapper;

    /// <summary>
    /// Represents app context containing tenant and other session-related details.
    /// </summary>
    protected readonly IAppContext AppContext = appContext;
}