// -----------------------------------------------------------------------
// <copyright file="SQSEventDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.SQSEvents;

/// <summary>
/// Represents SQS event.
/// </summary>
public class SqsEventDto
{
    /// <summary>
    /// Gets or sets message id.
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    /// Gets or sets body.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Gets or sets md5 hash of the body.
    /// </summary>
    public string Md5OfBody { get; set; }

    /// <summary>
    /// Gets or sets the event source arn.
    /// </summary>
    public string EventSourceArn { get; set; }
}