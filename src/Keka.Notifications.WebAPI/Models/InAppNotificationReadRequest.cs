// -----------------------------------------------------------------------
// <copyright file="InAppNotificationReadRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Keka.Notifications.WebAPI.Models;

/// <summary>
/// Represents in app notification read request model.
/// </summary>
public class InAppNotificationReadRequest
{
    /// <summary>
    /// Gets or Sets in app notification request Id.
    /// </summary>
    [JsonRequired]
    public string InAppNotificationId { get; set; }
}
