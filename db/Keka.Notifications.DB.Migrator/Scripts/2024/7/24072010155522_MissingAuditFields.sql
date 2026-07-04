IF [dbo].ColumnExists('ns.EmployeeNotificationPreference','CreatedBy') = 0
BEGIN
    PRINT 'Adding CreatedBy to the table: [ns].[EmployeeNotificationPreference]';
    ALTER TABLE [ns].[EmployeeNotificationPreference] 
    ADD [CreatedBy] UNIQUEIDENTIFIER NULL;
END;
GO

IF [dbo].ColumnExists('ns.EmployeeNotificationPreference','UpdatedOn') = 0
BEGIN
    PRINT 'Adding UpdatedOn to the table: [ns].[EmployeeNotificationPreference]';
    ALTER TABLE [ns].[EmployeeNotificationPreference] 
    ADD [UpdatedOn] DATETIME2 NULL;
END;
GO

IF [dbo].ColumnExists('ns.EmployeeNotificationPreference','UpdatedBy') = 0
BEGIN
    PRINT 'Adding UpdatedBy to the table: [ns].[EmployeeNotificationPreference]';
    ALTER TABLE [ns].[EmployeeNotificationPreference] 
    ADD [UpdatedBy] UNIQUEIDENTIFIER NULL;
END;
GO

IF [dbo].ColumnExists('ns.EmployeeNotificationPreference','IsDeleted') = 0
BEGIN
    PRINT 'Adding IsDeleted to the table: [ns].[EmployeeNotificationPreference]';
    ALTER TABLE [ns].[EmployeeNotificationPreference] 
    ADD [IsDeleted] BIT NOT NULL CONSTRAINT DF_EmployeeNotificationPreference_IsDeleted DEFAULT 0;
END;
GO

IF [dbo].ColumnExists('ns.EmployeeNotificationPreference','DeletedOn') = 0
BEGIN
    PRINT 'Adding DeletedOn to the table: [ns].[EmployeeNotificationPreference]';
    ALTER TABLE [ns].[EmployeeNotificationPreference] 
    ADD [DeletedOn] DATETIME2 NULL;
END;
GO

IF [dbo].ColumnExists('ns.EmployeeNotificationPreference','DeletedBy') = 0
BEGIN
    PRINT 'Adding DeletedBy to the table: [ns].[EmployeeNotificationPreference]';
    ALTER TABLE [ns].[EmployeeNotificationPreference] 
    ADD [DeletedBy] UNIQUEIDENTIFIER NULL;
END;
GO

IF [dbo].ColumnExists('ns.InAppNotificationRequest','IsDeleted') = 0
BEGIN
    PRINT 'Adding IsDeleted to the table: [ns].[InAppNotificationRequest]';
    ALTER TABLE [ns].[InAppNotificationRequest] 
    ADD [IsDeleted] BIT NOT NULL CONSTRAINT DF_InAppNotificationRequest_IsDeleted DEFAULT 0;
END;
GO

IF [dbo].ColumnExists('ns.InAppNotificationRequest','DeletedOn') = 0
BEGIN
    PRINT 'Adding DeletedOn to the table: [ns].[InAppNotificationRequest]';
    ALTER TABLE [ns].[InAppNotificationRequest] 
    ADD [DeletedOn] DATETIME2 NULL;
END;
GO

IF [dbo].ColumnExists('ns.InAppNotificationRequest','DeletedBy') = 0
BEGIN
    PRINT 'Adding DeletedBy to the table: [ns].[InAppNotificationRequest]';
    ALTER TABLE [ns].[InAppNotificationRequest] 
    ADD [DeletedBy] UNIQUEIDENTIFIER NULL;
END;
GO

IF [dbo].ColumnExists('ns.EmailStatus','LastBlockedOn') = 0
BEGIN
    PRINT 'Adding LastBlockedOn column to the table: [ns].[EmailStatus]';
    ALTER TABLE [ns].[EmailStatus] 
    ADD [LastBlockedOn] DATETIME2 NULL;
END;
GO

IF [dbo].ColumnExists('ns.EmailStatus','DeletedBy') = 0
BEGIN
    PRINT 'Adding DeletedBy column to the table: [ns].[EmailStatus]';
    ALTER TABLE [ns].[EmailStatus] 
    ADD [DeletedBy] UNIQUEIDENTIFIER NULL;
END;
GO

IF [dbo].ColumnExists('ns.PushNotificationRequest','IsDeleted') = 0
BEGIN
    PRINT 'Adding IsDeleted to the table: [ns].[PushNotificationRequest]';
    ALTER TABLE [ns].[PushNotificationRequest] 
    ADD [IsDeleted] BIT NOT NULL CONSTRAINT DF_PushNotificationRequest_IsDeleted DEFAULT 0;
END;
GO

IF [dbo].ColumnExists('ns.PushNotificationRequest','DeletedOn') = 0
BEGIN
    PRINT 'Adding DeletedOn to the table: [ns].[PushNotificationRequest]';
    ALTER TABLE [ns].[PushNotificationRequest] 
    ADD [DeletedOn] DATETIME2 NULL;
END;
GO

IF [dbo].ColumnExists('ns.PushNotificationRequest','DeletedBy') = 0
BEGIN
    PRINT 'Adding DeletedBy to the table: [ns].[PushNotificationRequest]';
    ALTER TABLE [ns].[PushNotificationRequest] 
    ADD [DeletedBy] UNIQUEIDENTIFIER NULL;
END;
GO

IF [dbo].ColumnExists('ns.SmsRequest','IsDeleted') = 0
BEGIN
    PRINT 'Adding IsDeleted to the table: [ns].[SmsRequest]';
    ALTER TABLE [ns].[SmsRequest] 
    ADD [IsDeleted] BIT NOT NULL CONSTRAINT DF_SmsRequest_IsDeleted DEFAULT 0;
END;
GO

IF [dbo].ColumnExists('ns.SmsRequest','DeletedOn') = 0
BEGIN
    PRINT 'Adding DeletedOn to the table: [ns].[SmsRequest]';
    ALTER TABLE [ns].[SmsRequest] 
    ADD [DeletedOn] DATETIME2 NULL;
END;
GO

IF [dbo].ColumnExists('ns.SmsRequest','DeletedBy') = 0
BEGIN
    PRINT 'Adding DeletedBy to the table: [ns].[SmsRequest]';
    ALTER TABLE [ns].[SmsRequest] 
    ADD [DeletedBy] UNIQUEIDENTIFIER NULL;
END;
GO

IF [dbo].ColumnExists('ns.WaMessageRequest','IsDeleted') = 0
BEGIN
    PRINT 'Adding IsDeleted to the table: [ns].[WaMessageRequest]';
    ALTER TABLE [ns].[WaMessageRequest] 
    ADD [IsDeleted] BIT NOT NULL CONSTRAINT DF_WaMessageRequest_IsDeleted DEFAULT 0;
END;
GO

IF [dbo].ColumnExists('ns.WaMessageRequest','DeletedOn') = 0
BEGIN
    PRINT 'Adding DeletedOn to the table: [ns].[WaMessageRequest]';
    ALTER TABLE [ns].[WaMessageRequest] 
    ADD [DeletedOn] DATETIME2 NULL;
END;
GO

IF [dbo].ColumnExists('ns.WaMessageRequest','DeletedBy') = 0
BEGIN
    PRINT 'Adding DeletedBy to the table: [ns].[WaMessageRequest]';
    ALTER TABLE [ns].[WaMessageRequest] 
    ADD [DeletedBy] UNIQUEIDENTIFIER NULL;
END;
GO