IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_Email') = 0
BEGIN
	PRINT 'Creating index [IX_EmailStatus_Email]';
	
    CREATE NONCLUSTERED INDEX [IX_EmailStatus_Email] ON [ns].[EmailStatus]
    (
        [Email] ASC
    )INCLUDE ([TenantId]) 
    WHERE ([IsDeleted]=(0))
    WITH (ONLINE = ON) ON [PRIMARY]
END
GO

IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_EMail_IsBlocked') = 0
BEGIN
	PRINT 'Creating index [IX_EmailStatus_EMail_IsBlocked]';
	
    CREATE NONCLUSTERED INDEX [IX_EmailStatus_EMail_IsBlocked] ON [ns].[EmailStatus] 
    (   [Email],
        [IsBlocked]
    ) INCLUDE ([BlockedUntil], [ConsecutiveBlockCount], [EmployeeId], [IsDeleted], [LastBlockedOn],
           [LastEmailDeliveredOn], [LastEmailFailedOn], [LastEmailFailedReason], [LastEmailSentOn],
           [NoOfEmailsFailed], [PreviousStateDetails], [TenantId]) 
    WHERE ([IsDeleted]=(0))
    WITH (ONLINE = ON) ON [PRIMARY]
END
GO
