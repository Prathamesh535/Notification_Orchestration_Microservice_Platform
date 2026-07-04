// -----------------------------------------------------------------------
// <copyright file="SqsMailDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.SQSEvents;

/// <summary>
/// Represents the email details.
/// </summary>
public class SqsMailDto
{
    /// <summary>
    /// Gets or sets the source email address.
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message identifier.
    /// </summary>
    public string MessageId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets tags.
    /// </summary>
    public Dictionary<string, List<string>> Tags { get; set; }
}
