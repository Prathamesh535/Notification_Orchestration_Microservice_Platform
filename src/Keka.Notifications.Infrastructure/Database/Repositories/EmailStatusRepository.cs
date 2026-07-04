// -----------------------------------------------------------------------
// <copyright file="EmailStatusRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Repository for handling CRUD operations related to email statuses.
/// </summary>
/// <seealso cref="Keka.Notifications.Infrastructure.Database.Repositories.BaseRepository" />
/// <seealso cref="Keka.Notifications.Core.Repositories.IEmailStatusRepository" />
internal class EmailStatusRepository : BaseRepository, IEmailStatusRepository
{
    private readonly IDateTimeService dateTimeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailStatusRepository"/> class.
    /// </summary>
    /// <param name="db">The database context to be used by the repository.</param>
    /// <param name="mapper">The mapper for converting between domain and database models.</param>
    /// <param name="appContext">The application context containing information about the current user and environment.</param>
    /// <param name="dateTimeService">The service providing date and time utilities.</param>
    public EmailStatusRepository(DatabaseContext db, IMapper mapper, IAppContext appContext, IDateTimeService dateTimeService)
        : base(db, mapper, appContext)
    {
        this.dateTimeService = dateTimeService;
    }

    /// <inheritdoc />
    public async Task<List<EmailStatus>> GetEmailStatusRecordsByEmailAsync(List<string> emailIds)
    {
        var emailStatuses = new List<EmailStatus>();
        foreach (var idsList in emailIds.SplitList(500))
        {
            var dbEmailStatus = await this.Db.Connection.QueryAsync<DbEmailStatus>(EmailStatusQueries.SelectEmailStatusByEmail, new { Emails = idsList });
            emailStatuses.AddRange(this.Mapper.Map<List<EmailStatus>>(dbEmailStatus));
        }

        return emailStatuses;
    }

    /// <inheritdoc />
    public async Task<List<EmailStatus>> GetEmailStatusIdByEmailAsync(List<string> emailIds)
    {
        var dbEmailStatus = await this.Db.Connection.QueryAsync<DbEmailStatus>(EmailStatusQueries.SelectEmailStatusIdByEmail, new { Emails = emailIds });
        return this.Mapper.Map<List<EmailStatus>>(dbEmailStatus);
    }

    /// <inheritdoc />
    public async Task<List<EmailStatus>> GetEmailStatusRecordsByEmployeeIdAsync(List<Guid> employeeIds)
    {
        var emailStatuses = new List<EmailStatus>();
        foreach (var idsList in employeeIds.SplitList(500))
        {
            var dbEmailStatus = await this.Db.Connection.QueryAsync<DbEmailStatus>(EmailStatusQueries.SelectEmailStatusByEmployee, new { EmployeeIds = idsList, TenantId = this.AppContext.TenantId });
            emailStatuses.AddRange(this.Mapper.Map<List<EmailStatus>>(dbEmailStatus));
        }

        return emailStatuses;
    }

    /// <inheritdoc />
    public async Task UpdateEmailStatusRecordAsync(EmailStatus emailStatusRecord)
    {
        var dbEmailStatusRecord = this.Mapper.Map<DbEmailStatus>(emailStatusRecord);
        dbEmailStatusRecord.SetAuditFieldsOnUpdate(this.AppContext);
        await this.Db.Connection.ExecuteAsync(EmailStatusQueries.UpdateEmailStatus, dbEmailStatusRecord);
    }

    /// <inheritdoc />
    public async Task UpdateEmailAsync(EmailStatus emailStatusRecord)
    {
        var dbEmailStatusRecord = this.Mapper.Map<DbEmailStatus>(emailStatusRecord);
        dbEmailStatusRecord.SetAuditFieldsOnUpdate(this.AppContext);
        await this.Db.Connection.ExecuteAsync(EmailStatusQueries.UpdateEmail, dbEmailStatusRecord);
    }

    /// <inheritdoc />
    public async Task<int> UnblockEmailsAsync(DateTime beforeCutOffDate)
    {
        var dbEmailStatus = new DbEmailStatus();
        dbEmailStatus.UpdatedOn = beforeCutOffDate;
        dbEmailStatus.SetAuditFieldsOnUpdate(this.AppContext);
        return await this.Db.Connection.ExecuteAsync(EmailStatusQueries.UnblockEmails, dbEmailStatus);
    }

    /// <inheritdoc />
    public async Task AddEmailStatusRecordAsync(EmailStatus emailStatusRecord)
    {
        var dbEmailStatusRecord = this.Mapper.Map<DbEmailStatus>(emailStatusRecord);
        dbEmailStatusRecord.SetAuditFieldsOnCreate(this.AppContext);

        await this.Db.Connection.ExecuteScalarAsync<Guid>(EmailStatusQueries.InsertEmailStatus, dbEmailStatusRecord);
    }

    /// <inheritdoc />
    public async Task<int> IncrementEmailSentCountAsync(EmailStatus emailStatusRecord)
    {
        var dbEmailStatusRecord = this.Mapper.Map<DbEmailStatus>(emailStatusRecord);
        dbEmailStatusRecord.SetAuditFieldsOnUpdate(this.AppContext);
        return await this.Db.Connection.ExecuteAsync(EmailStatusQueries.IncrementEmailSentCount, dbEmailStatusRecord);
    }

    /// <inheritdoc />
    public async Task<List<EmailStatus>> GetAllEmailStatusRecordsAsync()
    {
        var dbEmailStatusRecords = await this.Db.Connection.QueryAsync<DbEmailStatus>(EmailStatusQueries.GetAllEmailStatusRecords);
        return this.Mapper.Map<List<EmailStatus>>(dbEmailStatusRecords);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<string>> GetBlockedEmailsAsync(List<string> emailIds)
    {
        var parms = new { Emails = emailIds };
        return await this.Db.Connection.QueryAsync<string>(EmailStatusQueries.GetBlockedEmails, parms);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetEmailsToUnblockAsync(DateTime beforeCutOffDate)
    {
        return await this.Db.Connection.QueryAsync<string>(EmailStatusQueries.GetBlockedEmailsBeforeCutOffDate, new { CutOffDate = beforeCutOffDate });
    }

    /// <inheritdoc />
    public async Task<int> UnblockEmailAsync(string email)
    {
        var dbEmailStatusRecord = new DbEmailStatus()
        {
            Email = email,
        };
        dbEmailStatusRecord.SetAuditFieldsOnUpdate(this.AppContext);
        return await this.Db.Connection.ExecuteAsync(EmailStatusQueries.UnblockEmail, dbEmailStatusRecord);
    }

    /// <inheritdoc />
    public async Task<List<EmailStatus>> GetBlockedEmailsInPastDay()
    {
        var parms = new { LastBlockedOn = this.dateTimeService.GetCurrentTimeUtc().AddDays(-1) };
        var dbBlockedEmailStatusRecordsInPastDay = await this.Db.Connection.QueryAsync<DbEmailStatus>(EmailStatusQueries.GetBlockedEmployeeEmailsInPastDay, parms);
        return this.Mapper.Map<List<EmailStatus>>(dbBlockedEmailStatusRecordsInPastDay);
    }
}