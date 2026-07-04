IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_Email') = 1
BEGIN
	PRINT 'Dropping index [IX_EmailStatus_Email]';
	DROP INDEX [ns].[EmailStatus].IX_EmailStatus_Email;
END
GO

IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_TenantId_Email') = 0
BEGIN
	PRINT 'Adding index [IX_EmailStatus_TenantId_Email]';
	CREATE NONCLUSTERED INDEX [IX_EmailStatus_TenantId_Email] ON [ns].[EmailStatus]
	(
		[TenantId] ASC,
		[Email] ASC
	) WHERE [IsDeleted] = 0;
END
GO

IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_TenantId_EmployeeId') = 0
BEGIN
	PRINT 'Adding index [IX_EmailStatus_TenantId_EmployeeId]';
	CREATE NONCLUSTERED INDEX [IX_EmailStatus_TenantId_EmployeeId] ON [ns].[EmailStatus]
	(
		[TenantId] ASC,
		[EmployeeId] ASC
	) WHERE [IsDeleted] = 0;
END
GO

IF [dbo].[IndexExists]('ns.EmailStatus', 'IX_EmailStatus_IsBlocked') = 0
BEGIN
	PRINT 'Adding index [IX_EmailStatus_IsBlocked]';
	CREATE NONCLUSTERED INDEX [IX_EmailStatus_IsBlocked] ON [ns].[EmailStatus]
	(
		[IsBlocked] DESC
	) WHERE [IsDeleted] = 0;
END
GO