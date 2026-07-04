IF [dbo].ColumnExists('ns.Email','EmailRequestId') = 0
BEGIN
    PRINT 'Adding EmailRequestId column to the table: [ns].[Email]';

    ALTER TABLE [ns].[Email] 
    ADD [EmailRequestId] UNIQUEIDENTIFIER NULL;
END;
GO

IF [dbo].ColumnExists('ns.Email','ExternalId') = 0
BEGIN
    PRINT 'Adding ExternalId column to the table: [ns].[Email]';

    ALTER TABLE [ns].[Email] 
    ADD [ExternalId] VARCHAR(64) NULL;
END;
GO

IF [dbo].ColumnExists('ns.Email','Status') = 0
BEGIN
    PRINT 'Adding Status column to the table: [ns].[Email]';

    ALTER TABLE [ns].[Email] 
    ADD [Status] SMALLINT NOT NULL CONSTRAINT DF_Email_Status DEFAULT 0;
END;
GO