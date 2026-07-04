// -----------------------------------------------------------------------
// <copyright file="IEmailDeliveryHistoryRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the email delivery history repository.
/// </summary>
public interface IEmailDeliveryHistoryRepository
{
    /// <summary>
    /// Upsert Emails.
    /// </summary>
    /// <param name="emailDeliveryHistories">List of email delivery history records.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpsertEmailHistoryAsync(List<EmailDeliveryHistory> emailDeliveryHistories);

    /// <summary>
    /// Upserts email delivery raw data records.
    /// </summary>
    /// <param name="emailDeliveryRawDataRecords">A list of email delivery raw data records.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpsertEmailDeliveryRawDataAsync(List<EmailDeliveryRawData> emailDeliveryRawDataRecords);

    /// <summary>
    /// Gets Email delivery history records.
    /// </summary>
    /// <param name="getEmailHistoryRequest">The email history request.</param>
    /// <returns>Email delivery history records.</returns>
    Task<PagedResponse<EmailDeliveryHistory>> GetEmailDeliveryHistoryAsync(GetEmailHistoryRequest getEmailHistoryRequest);

    /// <summary>
    /// Gets raw email delivery history records.
    /// </summary>
    /// <param name="messageId">The message id.</param>
    /// <returns>Email delivery history records.</returns>
    Task<List<EmailDeliveryRawData>> GetRawEmailDeliveryHistoryAsync(string messageId);
}
