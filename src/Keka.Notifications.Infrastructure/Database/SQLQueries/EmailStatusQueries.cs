namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

internal static class EmailStatusQueries
{
    public const string SelectEmailStatusByEmail = @"
            SELECT 
                [EmailStatusId],
                [EmployeeId],
                [Email],
                [LastEmailDeliveredOn],
                [LastEmailSentOn],
                [LastEmailFailedOn],
                [LastEmailFailedReason],
                [ConsecutiveBlockCount],
                [LastBlockedOn],
                [PreviousStateDetails],
                [IsBlocked],
                [BlockedUntil],
                [NoOfEmailsFailed]
            FROM 
                [ns].[EmailStatus] WITH (NOLOCK)
            WHERE 
                [Email] IN @Emails";

    public const string SelectEmailStatusIdByEmail = @"
            SELECT 
                [EmailStatusId],
                [Email]
            FROM 
                [ns].[EmailStatus] WITH (NOLOCK)
            WHERE 
                [Email] IN @Emails";

    public const string SelectEmailStatusByEmployee = @"
            SELECT 
                [EmailStatusId],
                [EmployeeId],
                [Email],
                [NoOfEmailsSent],
                [LastEmailSentOn],
                [LastEmailDeliveredOn],
                [LastEmailFailedOn],
                [LastEmailFailedReason],
                [ConsecutiveBlockCount],
                [LastBlockedOn],
                [PreviousStateDetails],
                [IsBlocked],
                [BlockedUntil],
                [NoOfEmailsFailed]
            FROM 
                [ns].[EmailStatus] WITH (NOLOCK)
            WHERE 
                [TenantId] = @TenantId AND [EmployeeId] IN @EmployeeIds and IsDeleted = 0";

    public const string UpdateEmailStatus = @"
            UPDATE 
                [ns].[EmailStatus]
            SET
                [LastEmailDeliveredOn] = @LastEmailDeliveredOn,
                [LastEmailFailedOn] = @LastEmailFailedOn,
                [LastEmailFailedReason] = @LastEmailFailedReason,
                [ConsecutiveBlockCount] = @ConsecutiveBlockCount,
                [PreviousStateDetails] = @PreviousStateDetails,
                [IsBlocked] = @IsBlocked,
                [LastBlockedOn] = @LastBlockedOn,
                [BlockedUntil] = @BlockedUntil,
                [NoOfEmailsFailed] = @NoOfEmailsFailed,
                [UpdatedOn] = @UpdatedOn,
                [UpdatedBy] = @UpdatedBy
            WHERE
               [Email] = @Email";

    public const string UpdateEmail = @"
            UPDATE 
                [ns].[EmailStatus]
            SET
                [Email] = @Email,
                [UpdatedOn] = @UpdatedOn,
                [UpdatedBy] = @UpdatedBy
            WHERE
                [EmailStatusId] = @EmailStatusId";

    public const string UnblockEmails = @"
            EXEC sp_set_session_context @key=N'IsMigration', @value=1;
            UPDATE
                [ns] .[EmailStatus]
            SET
                [IsBlocked] = 0,
                [BlockedUntil] = NULL,
                [UpdatedOn] = @UpdatedOn,
                [UpdatedBy] = @UpdatedBy
            WHERE
                [IsBlocked] = 1 AND BlockedUntil IS NOT NULL AND BlockedUntil <= @UpdatedOn";

    public const string GetBlockedEmailsBeforeCutOffDate = @"
            EXEC sp_set_session_context @key=N'IsMigration', @value=1;
            SELECT
                [EMAIL]
            FROM 
                [ns].[EmailStatus] WITH (NOLOCK)
            WHERE
                [IsBlocked] = 1 AND [BlockedUntil] IS NOT NULL AND [BlockedUntil] <= @CutOffDate";

    public const string GetAllEmailStatusRecords = @"
            SELECT 
                    [EmailStatusId],
                    [EmployeeId],
                    [Email],
                    [NoOfEmailsSent],
                    [NoOfEmailsFailed],
                    [LastEmailSentOn],
                    [LastEmailDeliveredOn],
                    [LastEmailFailedOn],
                    [LastEmailFailedReason],
                    [ConsecutiveBlockCount],
                    [IsBlocked],
                    [LastBlockedOn],
                    [BlockedUntil]
                FROM 
                    [ns].[EmailStatus] WITH (NOLOCK)";

    public const string InsertEmailStatus = @"
            INSERT INTO [ns].[EmailStatus]
                   ([EmployeeId],
                    [TenantId],
                    [Email],
		            [NoOfEmailsSent],
                    [LastEmailDeliveredOn],
                    [LastEmailFailedOn],
                    [LastEmailFailedReason],
                    [ConsecutiveBlockCount],
                    [IsBlocked],
                    [LastBlockedOn],
                    [BlockedUntil],
                    [NoOfEmailsFailed],
                    [LastEmailSentOn],
                    [CreatedOn],
                    [CreatedBy]
                    )
             VALUES
                   (@EmployeeId,
                    @TenantId,
                    @Email,
		            @NoOfEmailsSent,
                    @LastEmailDeliveredOn,
                    @LastEmailFailedOn,
                    @LastEmailFailedReason,
                    @ConsecutiveBlockCount,
                    @IsBlocked,
                    @LastBlockedOn,
                    @BlockedUntil,
                    @NoOfEmailsFailed,
                    @LastEmailSentOn,
                    @CreatedOn,
                    @CreatedBy)";

    public const string IncrementEmailSentCount = @"
            UPDATE
                [ns].[EmailStatus]
            SET
                [NoOfEmailsSent] = [NoOfEmailsSent]+1,
                [LastEmailSentOn] = @LastEmailSentOn,
                [UpdatedOn] = @UpdatedOn,
                [UpdatedBy] = @UpdatedBy
            WHERE
                [EmailStatusId] = @EmailStatusId";

    public const string GetBlockedEmails = @"
            SELECT 
                    [Email]
                FROM 
                    [ns].[EmailStatus] WITH (NOLOCK)
                WHERE 
                    [Email] IN @Emails AND [IsBlocked]=1";

    public const string UnblockEmail = @"
            UPDATE [ns].[EmailStatus]
            SET
               [IsBlocked] = 0,
               [ConsecutiveBlockCount] = 0,
               [BlockedUntil] = NULL,
               [UpdatedOn] = @UpdatedOn,
               [UpdatedBy] = @UpdatedBy
            WHERE
                [Email] = @Email AND [IsBlocked] = 1";

    public const string GetBlockedEmployeeEmailsInPastDay = @"
            SELECT 
                    [EmailStatusId],
                    [EmployeeId],
                    [Email],
                    [NoOfEmailsSent],
                    [NoOfEmailsFailed],
                    [LastEmailSentOn],
                    [LastEmailDeliveredOn],
                    [LastEmailFailedOn],
                    [LastEmailFailedReason],
                    [ConsecutiveBlockCount],
                    [IsBlocked],
                    [LastBlockedOn],
                    [BlockedUntil]
                FROM 
                    [ns].[EmailStatus] WITH (NOLOCK)
             WHERE
                 IsBlocked = 1 AND LastBlockedOn IS NOT NULL AND LastBlockedOn >= @LastBlockedOn";
}