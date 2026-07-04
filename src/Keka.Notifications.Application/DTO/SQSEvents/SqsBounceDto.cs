// -----------------------------------------------------------------------
// <copyright file="SqsBounceDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.SQSEvents;

/// <summary>
/// Represents a bounce event in SQS.
/// </summary>
public class SqsBounceDto
{
    /// <summary>
    /// Gets or sets the recipients whose emails have bounced.
    /// </summary>
    public SqsRecipientsDto[] BouncedRecipients { get; set; }

    /// <summary>
    /// Gets or sets the type of the bounce.
    /// </summary>
    public string BounceType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the subtype of the bounce.
    /// </summary>
    public string BounceSubType { get; set; } = string.Empty;
}
