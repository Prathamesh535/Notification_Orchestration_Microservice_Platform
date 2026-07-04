// -----------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers;

/// <summary>
/// Represents the base controller for API controllers in the application.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BaseApiController"/> class.
/// </remarks>
[Authorize]
[ApiController]
public class BaseApiController
    : ControllerBase
{
}
