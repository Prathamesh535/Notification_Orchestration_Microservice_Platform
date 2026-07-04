IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_TenantId_Email') = 1
BEGIN
	PRINT 'Dropping index [IX_EmailStatus_TenantId_Email]';
	DROP INDEX [ns].[EmailStatus].IX_EmailStatus_TenantId_Email;
END
GO

IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_IsBlocked') = 1
BEGIN
	PRINT 'Dropping index [IX_EmailStatus_IsBlocked]';
	DROP INDEX [ns].[EmailStatus].IX_EmailStatus_IsBlocked;
END
GO

IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_TenantId_Email_IsBlocked') = 0
BEGIN
	PRINT 'Creating index [IX_EmailStatus_TenantId_Email_IsBlocked]';
	CREATE NONCLUSTERED INDEX [IX_EmailStatus_TenantId_Email_IsBlocked] ON [ns].[EmailStatus]
	(
		[TenantId] ASC,
		[Email] ASC,
		[IsBlocked] DESC
	) WHERE [IsDeleted] = 0;
END
GO
