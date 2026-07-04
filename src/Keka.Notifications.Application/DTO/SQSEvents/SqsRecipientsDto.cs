// -----------------------------------------------------------------------
// <copyright file="SqsRecipientsDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.SQSEvents;

/// <summary>
/// Represents an email recipient in SQS.
/// </summary>
public class SqsRecipientsDto
{
    /// <summary>
    /// Gets or sets the email address of the recipient.
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;
}
