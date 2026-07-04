// -----------------------------------------------------------------------
// <copyright file="UnblockEmailResponse.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.EmailMessages;

/// <summary>
/// Represents a response for the unblocking of email addresses.
/// </summary>
public class UnblockEmailResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the unblocking operation was successful.
    /// </summary>
    public bool Success { get; set; }

#nullable enable
    /// <summary>
    /// Gets or sets the error message describing any failure during the unblocking operation.
    /// </summary>
    public string? ErrorMessage { get; set; }
#nullable restore
}