// -----------------------------------------------------------------------
// <copyright file="EmailUnblockRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Models;

/// <summary>
/// Represents email unblock request model.
/// </summary>
public class EmailUnblockRequest
{
    /// <summary>
    /// Gets or sets email to be unclocked.
    /// </summary>
    public string Email { get; set; }
}
