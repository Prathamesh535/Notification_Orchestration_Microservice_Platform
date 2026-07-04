IF [dbo].ColumnExists('ns.EmailStatus','LastDeliveredExternalId') = 0
BEGIN
    PRINT 'Adding LastDeliveredExternalId column to the table: [ns].[EmailStatus]';

    ALTER TABLE [ns].[EmailStatus]
    ADD [LastDeliveredExternalId] NVARCHAR(60) NULL;
END;
GO

IF [dbo].ColumnExists('ns.EmailStatus','PreviousConsecutiveBlockCount') = 0
BEGIN
    PRINT 'Adding PreviousConsecutiveBlockCount column to the table: [ns].[EmailStatus]';

    ALTER TABLE [ns].[EmailStatus] 
    ADD [PreviousConsecutiveBlockCount] TINYINT NOT NULL CONSTRAINT DF_EmailStatus_PreviousConsecutiveBlockCount DEFAULT 0;
END;
GO