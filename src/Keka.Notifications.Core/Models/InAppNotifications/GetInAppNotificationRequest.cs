// -----------------------------------------------------------------------
// <copyright file="GetInAppNotificationRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.InAppNotifications;

/// <summary>
/// InAppNotificationRequest entity model.
/// </summary>
/// <seealso cref="Keka.Notifications.Core.Interfaces.IPageRequest" />
public class GetInAppNotificationRequest : IPageRequest
{
    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Gets or sets the continuation token.
    /// </summary>
    public string ContinuationToken { get; set; }
}
