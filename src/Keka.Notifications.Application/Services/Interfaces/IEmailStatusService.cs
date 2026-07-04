// -----------------------------------------------------------------------
// <copyright file="IEmailStatusService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Defines methods for saving email status.
/// </summary>
public interface IEmailStatusService
{
    /// <summary>
    /// Gets email status records.
    /// </summary>
    /// <param name="emailStatusRequest">The email status to request.</param>
    /// <returns>The async task that eventually returns list of all email statistics.</returns>
    Task<List<EmailStatusDto>> GetEmailStatusRecordsAsync(EmailStatusRequestDto emailStatusRequest);

    /// <summary>
    /// Unblocks email by email address.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <returns>The async task that eventually returns true if email is unblocked.</returns>
    Task<bool> UnblockEmailAsync(string email);

    /// <summary>
    /// Unblocks emails based on a cutoff date.
    /// </summary>
    /// <returns>The async task that eventually returns integer that represents no of users unblocked.</returns>
    Task<int> UnblockEmailsAsync();

    /// <summary>
    /// Gets the blocked employee emails in past day.
    /// </summary>
    /// <returns>The async task that eventually returns list of all blocked email statistics in past one day.</returns>
    Task<List<EmailStatusDto>> GetBlockedEmailsInPastDay();

    /// <summary>
    /// Adds employee Id to email status.
    /// </summary>
    /// <param name="employeeAddedEvent">The employee added event.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddEmailStatusAsync(EmployeeAddedEvent employeeAddedEvent);

    /// <summary>
    /// Updates employee Id, email in email status.
    /// </summary>
    /// <param name="employeeEmailUpdatedEvent">The employee email updated event.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateEmailAsync(EmployeeEmailUpdatedEvent employeeEmailUpdatedEvent);
}
