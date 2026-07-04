// -----------------------------------------------------------------------
// <copyright file="IEmailDeliveryConverter.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Helpers.Interfaces;

/// <summary>
/// Represents email delivery converter interface.
/// </summary>
public interface IEmailDeliveryConverter
{
    /// <summary>
    /// Converts email to email delivery history records.
    /// </summary>
    /// <param name="syncEmailEvent">Email sync event.</param>
    /// <param name="email">Email record.</param>
    /// <returns>List of email history records.</returns>
    List<EmailDeliveryHistory> ConvertToEmailHistory(SyncEmailEvent syncEmailEvent, Email email);

    /// <summary>
    /// Converts sqs event dtos to email delivery history records.
    /// </summary>
    /// <param name="sqsEventDtos">Sqs event dtos.</param>
    /// <param name="emailDeliveryRawDataRecords">Email delivery raw data records.</param>
    /// <returns>List of email delivery history records.</returns>
    List<EmailDeliveryHistory> ConvertToEmailHistory(List<SqsEventDto> sqsEventDtos, out List<EmailDeliveryRawData> emailDeliveryRawDataRecords);
}
