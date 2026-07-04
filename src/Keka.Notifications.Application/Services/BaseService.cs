// -----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents the base service.
/// </summary>
public abstract class BaseService : IBaseService
{
    /// <summary>
    /// Represents the logger instance.
    /// </summary>
    protected readonly IMapper Mapper;

    /// <summary>
    /// Represents the automapper instance.
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// Represents the unit of work.
    /// </summary>
    protected readonly IUnitOfWork UnitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="mapper">The automapper instance.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    protected BaseService(ILogger logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }
}
