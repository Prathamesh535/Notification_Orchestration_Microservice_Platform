IF [dbo].ColumnExists('ns.EmailStatus','LastDeliveredExternalId') = 1
BEGIN
    PRINT 'Droping LastDeliveredExternalId from the table: [ns].[EmailStatus]';

    ALTER TABLE [ns].[EmailStatus] DROP COLUMN [LastDeliveredExternalId];
END;
GO

IF [dbo].ColumnExists('ns.EmailStatus','LastMessageExternalId') = 1
BEGIN
    PRINT 'Droping LastMessageExternalId from the table: [ns].[EmailStatus]';

    ALTER TABLE [ns].[EmailStatus] DROP COLUMN [LastMessageExternalId];
END;
GO

IF [dbo].ColumnExists('ns.EmailStatus','PreviousConsecutiveBlockCount') = 1
BEGIN
    PRINT 'Droping PreviousConsecutiveBlockCount from the table: [ns].[EmailStatus]';

    ALTER TABLE [ns].[EmailStatus] DROP CONSTRAINT [DF_EmailStatus_PreviousConsecutiveBlockCount];
    ALTER TABLE [ns].[EmailStatus] DROP COLUMN [PreviousConsecutiveBlockCount];
END;
GO

IF [dbo].ColumnExists('ns.EmailStatus','PreviousStateDetails') = 0
BEGIN
    PRINT 'Adding PreviousStateDetails column to the table: [ns].[EmailStatus]';

    ALTER TABLE [ns].[EmailStatus] 
    ADD [PreviousStateDetails] NVARCHAR(500) NULL;
END;
GO