CREATE TABLE [ns].[EmployeeNotificationPreference]
(
    [EmployeeId]    UNIQUEIDENTIFIER    NOT NULL,
    [EventId]       UNIQUEIDENTIFIER    NOT NULL,
    [TenantId]      UNIQUEIDENTIFIER    NOT NULL,
    [IsEnabled]     BIT                 NOT NULL    CONSTRAINT [DF_EmployeeNotificationPreference_IsEnabled] DEFAULT 0,
    [CreatedOn]     DATETIME2           NOT NULL    CONSTRAINT [DF_EmployeeNotificationPreference_CreatedOn] DEFAULT GetUtcDate(),
    [CreatedBy]     UNIQUEIDENTIFIER    NOT NULL,
    [UpdatedOn]     DATETIME2           NULL, 
	[UpdatedBy]     UNIQUEIDENTIFIER    NULL,
    [IsDeleted]     BIT                 NOT NULL    CONSTRAINT DF_EmployeeNotificationPreference_IsDeleted DEFAULT 0, 
	[DeletedOn]     DATETIME2           NULL,
	[DeletedBy]     UNIQUEIDENTIFIER    NULL,
    CONSTRAINT [PK_EmployeeNotificationPreference_EmployeeId_EventId] PRIMARY KEY ([EmployeeId],[EventId]),
    CONSTRAINT [FK_EmployeeNotificationPreference_EventId] FOREIGN KEY ([EventId]) REFERENCES [ns].[Event]([EventId])
)
GO

CREATE NONCLUSTERED INDEX [IX_EmployeeNotificationPreference_TenantId] ON [ns].[EmployeeNotificationPreference]
(
    [TenantId] ASC, [EmployeeId] ASC
) WHERE IsDeleted = 0;
GO

CREATE SECURITY POLICY [ns].[EmployeeNotificationPreferenceAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[EmployeeNotificationPreference],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[EmployeeNotificationPreference]
	WITH (STATE = ON);
GO