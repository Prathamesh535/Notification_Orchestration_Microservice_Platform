IF [dbo].[TableExists]('[ns].[EmailRequest]') != 0
BEGIN
	PRINT 'Updating table: [ns].[EmailRequest]';
	
	ALTER TABLE [ns].[EmailRequest] ALTER COLUMN [Content] NVARCHAR(MAX) NULL
END
GO

IF [dbo].[TableExists]('[ns].[Email]') != 0 AND [dbo].[ConstraintExists]('[ns].[Email]','[FK_Email_Tenant]') != 0
BEGIN
	PRINT 'Updating table: [ns].[Email]';
	
	ALTER TABLE [ns].[Email] DROP CONSTRAINT [FK_Email_Tenant]
END
GO

IF [dbo].[TableExists]('[ns].[Employee]') != 0 AND [dbo].[ConstraintExists]('[ns].[Employee]','[FK_Employee_Tenant]') != 0
BEGIN
	PRINT 'Updating table: [ns].[Employee]';
	
	ALTER TABLE [ns].[Employee] DROP CONSTRAINT [FK_Employee_Tenant]
END
GO
