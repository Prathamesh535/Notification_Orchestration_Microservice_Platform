// -----------------------------------------------------------------------
// <copyright file="IEmailStatusRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Defines methods for accessing and manipulating email status in the repository.
/// </summary>
public interface IEmailStatusRepository
{
    /// <summary>
    /// Gets email status records for given email ids.
    /// </summary>
    /// <param name="emailIds">The list of emails.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains list of email status records.</returns>
    Task<List<EmailStatus>> GetEmailStatusRecordsByEmailAsync(List<string> emailIds);

    /// <summary>
    /// Gets email status id for given email ids.
    /// </summary>
    /// <param name="emailIds">The list of emails.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains list of email status records.</returns>
    Task<List<EmailStatus>> GetEmailStatusIdByEmailAsync(List<string> emailIds);

    /// <summary>
    /// Gets email status records for given employee ids.
    /// </summary>
    /// <param name="employeeIds">The list of employee ids.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains list of email status records.</returns>
    Task<List<EmailStatus>> GetEmailStatusRecordsByEmployeeIdAsync(List<Guid> employeeIds);

    /// <summary>
    /// Asynchronously saves the provided email status.
    /// </summary>
    /// <param name="emailStatusRecord">The email status to save.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task UpdateEmailStatusRecordAsync(EmailStatus emailStatusRecord);

    /// <summary>
    /// Updates email in email status.
    /// </summary>
    /// <param name="emailStatusRecord">The email status records to update.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    Task UpdateEmailAsync(EmailStatus emailStatusRecord);

    /// <summary>
    /// Creates new email status record.
    /// </summary>
    /// <param name="emailStatusRecord">Email status record.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    Task AddEmailStatusRecordAsync(EmailStatus emailStatusRecord);

    /// <summary>
    /// Increments email sent count.
    /// </summary>
    /// <param name="emailStatusRecord">Email status record.</param>
    /// <returns>The task result which retuns no of affected rows.</returns>
    Task<int> IncrementEmailSentCountAsync(EmailStatus emailStatusRecord);

    /// <summary>
    /// Unblocks emails which are blocked.
    /// </summary>
    /// <param name="beforeCutOffDate">Datetime threshold based on wihch unblocking happens.</param>
    /// <returns>A async task that eventually returns integer that returns no of email records unblocked.</returns>
    Task<int> UnblockEmailsAsync(DateTime beforeCutOffDate);

    /// <summary>
    /// Gives blocked emails.
    /// </summary>
    /// <param name="emailIds">List of email ids.</param>
    /// <returns>The task which eventually returns list of blocked email ids from given list.</returns>
    Task<IEnumerable<string>> GetBlockedEmailsAsync(List<string> emailIds);

    /// <summary>
    /// Get all email status records.
    /// </summary>
    /// <returns>List of all email status records.</returns>
    Task<List<EmailStatus>> GetAllEmailStatusRecordsAsync();

    /// <summary>
    /// Unblocks emails by email address.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <returns>A async task that eventually returns integer that returns no of email records unblocked.</returns>
    Task<int> UnblockEmailAsync(string email);

    /// <summary>
    /// Retrieves emails that need to be unblocked based on a cutoff date.
    /// </summary>
    /// <param name="beforeCutOffDate">The before cut off date.</param>
    /// <returns> Collection of emails to unblock.</returns>
    Task<IEnumerable<string>> GetEmailsToUnblockAsync(DateTime beforeCutOffDate);

    /// <summary>
    /// Gets the blocked employee emails in past day.
    /// </summary>
    /// <returns>List of all blocked email status records in past one day.</returns>
    Task<List<EmailStatus>> GetBlockedEmailsInPastDay();
}