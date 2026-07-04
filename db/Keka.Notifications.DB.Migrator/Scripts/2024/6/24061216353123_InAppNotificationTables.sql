IF [dbo].[TableExists]('[ns].[InAppNotificationRequest]') = 0
BEGIN
	PRINT 'Creating table [ns].[InAppNotificationRequest]';
	
	CREATE TABLE [ns].[InAppNotificationRequest]
(
	[InAppNotificationRequestId]    UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_InAppNotificationRequest_InAppNotificationRequestId DEFAULT NEWSEQUENTIALID(),
	[TenantId]						UNIQUEIDENTIFIER    NOT NULL,
	[Personalization]				NVARCHAR(MAX)       NULL,
	[InAppNotificationTemplateId]   UNIQUEIDENTIFIER    NULL,
	[Status]						SMALLINT			NOT NULL    CONSTRAINT DF_InAppNotificationRequest_Status DEFAULT 0,
	[FailureReason]					NVARCHAR(500)		NULL,
	[CreatedOn]						DATETIME2           NOT NULL    CONSTRAINT DF_InAppNotificationRequest_CreatedOn DEFAULT GETUTCDATE(), 
	[CreatedBy]						UNIQUEIDENTIFIER    NOT NULL, 
	[UpdatedOn]						DATETIME2           NULL, 
	[UpdatedBy]						UNIQUEIDENTIFIER    NULL,
	CONSTRAINT [PK_InAppNotificationRequest] PRIMARY KEY ([InAppNotificationRequestId])
)

CREATE NONCLUSTERED INDEX [IX_InAppNotificationRequest_TenantId] ON [ns].[InAppNotificationRequest]
(
	[TenantId] ASC
)
END
GO

IF [dbo].[SecurityPolicyExists]('InAppNotificationRequestAccessPolicy') = 0
BEGIN
    -- If the security policy does not exist, create it
    PRINT 'Creating security policy: InAppNotificationRequestAccessPolicy';
    
	CREATE SECURITY POLICY [ns].[InAppNotificationRequestAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[InAppNotificationRequest],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[InAppNotificationRequest]
	WITH (STATE = ON);
END
GO

IF [dbo].[TableExists]('[ns].[InAppNotificationTemplate]') = 0
BEGIN
	PRINT 'Creating table [ns].[InAppNotificationTemplate]';
	
	CREATE TABLE [ns].[InAppNotificationTemplate]
	(
		[InAppNotificationTemplateId]   UNIQUEIDENTIFIER	NOT NULL CONSTRAINT DF_InAppNotificationTemplate_InAppNotificationTemplateId DEFAULT NEWSEQUENTIALID(), 
		[Name]							NVARCHAR(100)		NOT NULL,
		[Title]							NVARCHAR(100)		NOT NULL,
		[Body]                          NVARCHAR(MAX)		NULL,
		[Parameters]                    NVARCHAR(MAX)		NULL,
		[CreatedOn]                     DATETIME2			NOT NULL CONSTRAINT [DF_InAppNotificationTemplate_CreatedOn] DEFAULT GETUTCDATE(), 
		[CreatedBy]                     UNIQUEIDENTIFIER	NOT NULL,
		[UpdatedOn]                     DATETIME2			NULL,
		[UpdatedBy]                     UNIQUEIDENTIFIER	NULL, 
		CONSTRAINT [PK_InAppNotificationTemplate] PRIMARY KEY CLUSTERED ([InAppNotificationTemplateId] ASC)
	)
END
GO

IF [dbo].[TableExists]('[ns].[Module]') = 0
BEGIN
	PRINT 'Creating table [ns].[Module]';

	CREATE TABLE [ns].[Module]
	(
		[ModuleId]      UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_Module_ModuleId DEFAULT NEWSEQUENTIALID(),
		[Name]          NVARCHAR(100)       NOT NULL,
		[Description]   NVARCHAR(255)       NULL,
		[CreatedOn]     DATETIME2           NOT NULL	CONSTRAINT DF_Module_CreatedOn DEFAULT GETUTCDATE(),
		[CreatedBy]     UNIQUEIDENTIFIER    NOT NULL,
		CONSTRAINT [PK_Module] PRIMARY KEY ([ModuleId])
	)
END
GO

IF [dbo].[TableExists]('[ns].[Event]') = 0
BEGIN
	PRINT 'Creating table [ns].[Event]';

	CREATE TABLE [ns].[Event]
	(
		[EventId]       UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_Event_EventId DEFAULT NEWSEQUENTIALID(),
		[ModuleId]      UNIQUEIDENTIFIER    NOT NULL,
		[EventCode]     NVARCHAR(100)       NULL,
		[Name]          NVARCHAR(100)       NOT NULL,
		[Description]   NVARCHAR(255)       NULL,
		[CreatedOn]     DATETIME2           NOT NULL    CONSTRAINT DF_Event_CreatedOn DEFAULT GETUTCDATE(),
		[CreatedBy]     UNIQUEIDENTIFIER    NOT NULL,
		CONSTRAINT [PK_Event] PRIMARY KEY ([EventId]),
		CONSTRAINT [FK_Event_ModuleId] FOREIGN KEY (ModuleId) REFERENCES [ns].[Module]([ModuleId])
	)
END
GO

IF [dbo].[TableExists]('[ns].[EmployeeNotificationPreference]') = 0
BEGIN
    PRINT 'Creating table [ns].[EmployeeNotificationPreference]';

    CREATE TABLE [ns].[EmployeeNotificationPreference]
    (
        [EmployeeId]                       UNIQUEIDENTIFIER    NOT NULL,
        [EventId]                          UNIQUEIDENTIFIER    NOT NULL,
        [TenantId]                         UNIQUEIDENTIFIER    NOT NULL,
		[IsEnabled]                        BIT                 NOT NULL    CONSTRAINT [DF_EmployeeNotificationPreference_IsEnabled] DEFAULT 0,
        [CreatedOn]                        DATETIME2           NOT NULL    CONSTRAINT [DF_EmployeeNotificationPreference_CreatedOn] DEFAULT GetUtcDate(),
		CONSTRAINT [PK_EmployeeNotificationPreference_EmployeeId_EventId] PRIMARY KEY ([EmployeeId],[EventId]),
        CONSTRAINT [FK_EmployeeNotificationPreference_EventId] FOREIGN KEY ([EventId]) REFERENCES [ns].[Event]([EventId])
    )

    CREATE NONCLUSTERED INDEX [IX_EmployeeNotificationPreference_TenantId] ON [ns].[EmployeeNotificationPreference]
    (
        [TenantId] ASC, [EmployeeId] ASC
    );
END
GO


IF [dbo].[SecurityPolicyExists]('NotificationPreferenceAccessPolicy') = 0
BEGIN
    -- If the security policy does not exist, create it
    PRINT 'Creating security policy: NotificationPreferenceAccessPolicy';
    
	CREATE SECURITY POLICY [ns].[EmployeeNotificationPreferenceAccessPolicy]
		ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[EmployeeNotificationPreference],
		ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[EmployeeNotificationPreference]
		WITH (STATE = ON);
END
GO