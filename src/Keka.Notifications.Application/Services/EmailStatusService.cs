// -----------------------------------------------------------------------
// <copyright file="EmailStatusService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Initializes a new instance of the <see cref="EmailStatusService"/> class.
/// </summary>
public class EmailStatusService : IEmailStatusService
{
    private readonly IEmailStatusRepository emailStatusRepository;
    private readonly ILogger<EmailStatusService> logger;
    private readonly IMapper mapper;
    private readonly IDateTimeService dateTimeService;
    private readonly IEmailProvider emailProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailStatusService"/> class.
    /// </summary>
    /// <param name="logger">The logger service.</param>
    /// <param name="mapper">The mapper instance.</param>
    /// <param name="emailStatusRepository">The email status repository instance.</param>
    /// <param name="dateTimeService">The date time service instance.</param>
    /// <param name="emailProvider">The email provider instance.</param>
    public EmailStatusService(ILogger<EmailStatusService> logger, IMapper mapper, IEmailStatusRepository emailStatusRepository, IDateTimeService dateTimeService, IEmailProvider emailProvider)
    {
        this.logger = logger;
        this.emailStatusRepository = emailStatusRepository;
        this.mapper = mapper;
        this.dateTimeService = dateTimeService;
        this.emailProvider = emailProvider;
    }

    /// <inheritdoc/>
    public async Task<List<EmailStatusDto>> GetEmailStatusRecordsAsync(EmailStatusRequestDto emailStatusRequest)
    {
        List<EmailStatus> emailStatusRecords = null;
        if (emailStatusRequest is null || emailStatusRequest.EmployeeIds is null || emailStatusRequest.EmployeeIds.Count == 0)
        {
            emailStatusRecords = await this.emailStatusRepository.GetAllEmailStatusRecordsAsync();
        }
        else
        {
            emailStatusRecords = await this.emailStatusRepository.GetEmailStatusRecordsByEmployeeIdAsync(emailStatusRequest.EmployeeIds);
        }

        return this.mapper.Map<List<EmailStatusDto>>(emailStatusRecords);
    }

    /// <inheritdoc/>
    public async Task<int> UnblockEmailsAsync()
    {
        // Determine the cutoff date for unblocking emails
        var cutoffDate = this.dateTimeService.GetCurrentTimeUtc().Date;

        // Retrieve the list of email addresses that are blocked until the cutoff date
        var blockedEmails = (await this.emailStatusRepository.GetEmailsToUnblockAsync(cutoffDate)).Distinct().ToList();

        // Update the repository to mark emails as unblocked based on the cutoff date
        int unblockedRecordCount = await this.emailStatusRepository.UnblockEmailsAsync(cutoffDate);

        // Unblock the email in the external service (e.g., Amazon SES)
        if (blockedEmails.Count > 0)
        {
            await this.emailProvider.UnblockAsync(blockedEmails);
        }

        // Return response
        return unblockedRecordCount;
    }

    /// <inheritdoc />
    public async Task<bool> UnblockEmailAsync(string email)
    {
        // Unblock the email in the database and get the number of records affected
        var noOfEmailRecordsUnblocked = await this.emailStatusRepository.UnblockEmailAsync(email);

        // Unblock the email in the external service (e.g., Amazon SES)
        var unblockResponse = await this.emailProvider.UnblockAsync(email);
        if (!unblockResponse.Success)
        {
            this.logger.LogError("Unblock failed for the email {email} with message: {message}", email, unblockResponse.ErrorMessage);
        }

        // Return true if at least one record was unblocked
        return noOfEmailRecordsUnblocked > 0;
    }

    /// <inheritdoc />
    public async Task<List<EmailStatusDto>> GetBlockedEmailsInPastDay()
    {
        var blockedEmailStatusRecordsInPastDay = await this.emailStatusRepository.GetBlockedEmailsInPastDay();
        return this.mapper.Map<List<EmailStatusDto>>(blockedEmailStatusRecordsInPastDay);
    }

    /// <inheritdoc />
    public async Task AddEmailStatusAsync(EmployeeAddedEvent employeeAddedEvent)
    {
        // Validate event
        ErrorCode? errorCode;
        string errorMessage;
        if (!ValidateEmployeeAddedEvent(employeeAddedEvent, out errorCode, out errorMessage))
        {
            throw new Exceptions.ApplicationException((ErrorCode)errorCode, errorMessage);
        }

        // Check if record exists with given employee id.
        var emailStatusRecord = (await this.emailStatusRepository.GetEmailStatusRecordsByEmployeeIdAsync(new List<Guid>() { employeeAddedEvent.EmployeeId })).FirstOrDefault()
                                ?? (await this.emailStatusRepository.GetEmailStatusRecordsByEmailAsync(new List<string> { employeeAddedEvent.Email })).FirstOrDefault();

        if (emailStatusRecord is not null)
        {
            this.logger.LogWarning("Employee with {empId} already exists in email status table.", employeeAddedEvent.EmployeeId);
            return;
        }

        // Add record if doesn't exist
        emailStatusRecord = new EmailStatus()
        {
            EmployeeId = employeeAddedEvent.EmployeeId,
            Email = employeeAddedEvent.Email,
        };
        await this.emailStatusRepository.AddEmailStatusRecordAsync(emailStatusRecord);
    }

    /// <inheritdoc />
    public async Task UpdateEmailAsync(EmployeeEmailUpdatedEvent employeeEmailUpdatedEvent)
    {
        // Validate event
        ErrorCode? errorCode;
        string errorMessage;
        if (!ValidateEmployeeEmailUpdatedEvent(employeeEmailUpdatedEvent, out errorCode, out errorMessage))
        {
            throw new Exceptions.ApplicationException((ErrorCode)errorCode, errorMessage);
        }

        // Check if record exists with given employee id.
        var emailStatusRecord = (await this.emailStatusRepository.GetEmailStatusRecordsByEmployeeIdAsync(new List<Guid>() { employeeEmailUpdatedEvent.EmployeeId })).FirstOrDefault();
        if (emailStatusRecord == null)
        {
            this.logger.LogError("Employee with {empId} doesn't exists in email status table.", employeeEmailUpdatedEvent.EmployeeId);
            return;
        }

        // Update email
        emailStatusRecord.Email = employeeEmailUpdatedEvent.NewEmail;
        await this.emailStatusRepository.UpdateEmailAsync(emailStatusRecord);
    }

    private static bool ValidateEmployeeEmailUpdatedEvent(EmployeeEmailUpdatedEvent employeeEmailUpdatedEvent, out ErrorCode? errorCode, out string errorMessage)
    {
        errorCode = null;
        errorMessage = null;
        if (employeeEmailUpdatedEvent.EmployeeId.Equals(Guid.Empty))
        {
            errorCode = ErrorCode.INVALID_ARGS;
            errorMessage = "EmployeeId is mandatory.";
            return false;
        }

        if (string.IsNullOrEmpty(employeeEmailUpdatedEvent.NewEmail))
        {
            errorCode = ErrorCode.INVALID_ARGS;
            errorMessage = "New email should not be empty";
            return false;
        }

        return true;
    }

    private static bool ValidateEmployeeAddedEvent(EmployeeAddedEvent employeeAddedEvent, out ErrorCode? errorCode, out string errorMessage)
    {
        errorCode = null;
        errorMessage = null;
        if (string.IsNullOrEmpty(employeeAddedEvent.Email))
        {
            errorCode = ErrorCode.INVALID_ARGS;
            errorMessage = "Email should not be empty";
            return false;
        }

        if (employeeAddedEvent.EmployeeId.Equals(Guid.Empty))
        {
            errorCode = ErrorCode.INVALID_ARGS;
            errorMessage = "Employee Id should not be empty";
            return false;
        }

        return true;
    }
}