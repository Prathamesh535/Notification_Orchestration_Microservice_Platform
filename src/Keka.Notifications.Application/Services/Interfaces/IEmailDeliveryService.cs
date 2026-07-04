// -----------------------------------------------------------------------
// <copyright file="IEmailDeliveryService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents email delivery service interface.
/// </summary>
public interface IEmailDeliveryService
{
    /// <summary>
    /// Syncs Email records to tabe storage.
    /// </summary>
    /// <param name="syncEmailEvent">The sync email event.</param>
    /// <returns>The task result.</returns>
    public Task SyncEmailAsync(SyncEmailEvent syncEmailEvent);

    /// <summary>
    /// Syncs sqs events to table storage.
    /// </summary>
    /// <param name="sqsEvents">List of sqs event dtos.</param>
    /// <returns>The task result.</returns>
    Task SyncEmailDeliveryEventsAsync(List<SqsEventDto> sqsEvents);

    /// <summary>
    /// Gets the email delivery history records.
    /// </summary>
    /// <param name="getEmailHistoryRequest">The email history request.</param>
    /// <returns>The paged email delivery history records.</returns>
    Task<PagedResponse<EmailDeliveryHistoryDto>> GetEmailDeliveryHistoryAsync(GetEmailHistoryRequest getEmailHistoryRequest);

    /// <summary>
    /// Gets the raw email delivery history records.
    /// </summary>
    /// <param name="messageId">The message id.</param>
    /// <returns>The paged email delivery history records.</returns>
    Task<List<EmailDeliveryRawDataBasicDetailsDto>> GetEmailDeliveryDataByMessageIdAsync(string messageId);
}