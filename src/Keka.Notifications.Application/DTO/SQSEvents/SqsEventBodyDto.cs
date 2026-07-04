// -----------------------------------------------------------------------
// <copyright file="SqsEventBodyDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.SQSEvents;

/// <summary>
/// Represents the SQS event body data transfer object.
/// </summary>
public class SqsEventBodyDto
{
    /// <summary>
    /// Gets or sets the message details.
    /// </summary>
    public string Message { get; set; }
}
