// -----------------------------------------------------------------------
// <copyright file="SqsComplaintDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.SQSEvents;

/// <summary>
/// Represents a complaint event in SQS.
/// </summary>
public class SqsComplaintDto
{
    /// <summary>
    /// Gets or sets the recipients who have complained.
    /// </summary>
    public SqsRecipientsDto[] ComplainedRecipients { get; set; }

    /// <summary>
    /// Gets or sets the type of feedback for the complaint.
    /// </summary>
    public string ComplaintFeedbackType { get; set; } = string.Empty;
}
