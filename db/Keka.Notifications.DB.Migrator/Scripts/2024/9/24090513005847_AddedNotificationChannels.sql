IF [dbo].ColumnExists('ns.Event','NotificationChannels') = 0
BEGIN
    PRINT 'Adding NotificationChannels column to the table: [ns].[Event]';

    ALTER TABLE [ns].[Event] 
    ADD [NotificationChannels] VARCHAR(MAX) NULL;
END;
GO