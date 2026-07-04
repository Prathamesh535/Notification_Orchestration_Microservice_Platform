// -----------------------------------------------------------------------
// <copyright file="SqsDeliveryDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.SQSEvents;

/// <summary>
/// Represents the delivery details.
/// </summary>
public class SqsDeliveryDto
{
    /// <summary>
    /// Gets or sets the recipients of the delivery.
    /// </summary>
    public string[] Recipients { get; set; }
}
