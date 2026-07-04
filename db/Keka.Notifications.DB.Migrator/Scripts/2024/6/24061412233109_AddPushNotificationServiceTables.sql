IF [dbo].[TableExists]('[ns].[PushNotificationTemplate]') = 0
BEGIN
    -- If the table does not exist, create it
    PRINT 'Creating table: [ns].[PushNotificationTemplate]';
    CREATE TABLE [ns].[PushNotificationTemplate]
    (
        [PushNotificationTemplateId] UNIQUEIDENTIFIER   NOT NULL CONSTRAINT DF_PushNotificationTemplate_PushNotificationTemplateId DEFAULT NEWSEQUENTIALID(),
        [Name]                       NVARCHAR(50)       NOT NULL,
        [Title]                      NVARCHAR(100)      NOT NULL,
        [Body]                       NVARCHAR(MAX)      NOT NULL,
        [TemplateParameters]         NVARCHAR(MAX)      NULL,  
        [DataParameters]             NVARCHAR(MAX)      NULL,  
        [CreatedOn]                  DATETIME2          NOT NULL CONSTRAINT DF_PushNotificationTemplate_CreatedOn DEFAULT GETUTCDATE(),
        [CreatedBy]                  UNIQUEIDENTIFIER   NOT NULL,
        [UpdatedOn]                  DATETIME2          NULL,
        [UpdatedBy]                  UNIQUEIDENTIFIER   NULL,
        CONSTRAINT [PK_PushNotificationTemplate] PRIMARY KEY ([PushNotificationTemplateId])
    );
END
GO

IF [dbo].[TableExists]('[ns].[PushNotificationRequest]') = 0
BEGIN
    -- If the table does not exist, create it
    PRINT 'Creating table: [ns].[PushNotificationRequest]';
    CREATE TABLE [ns].[PushNotificationRequest]
    (
        [PushNotificationRequestId]        UNIQUEIDENTIFIER    NOT NULL       CONSTRAINT DF_PushNotificationRequest_PushNotificationRequestId DEFAULT NEWSEQUENTIALID(),
        [TenantId]                         UNIQUEIDENTIFIER    NOT NULL,
        [PushNotificationTemplateId]       UNIQUEIDENTIFIER    NOT NULL,
        [Personalization]                  NVARCHAR(MAX)       NOT NULL,  
        [Status]                           SMALLINT            NOT NULL       CONSTRAINT DF_PushNotificationRequest_Status DEFAULT 0,
        [FailureReason]                    NVARCHAR(500)       NULL,
        [ExternalId]                       NVARCHAR(64)        NULL,
        [CreatedOn]                        DATETIME2           NOT NULL       CONSTRAINT DF_PushNotificationRequest_CreatedOn DEFAULT SYSUTCDATETIME(),
        [CreatedBy]                        UNIQUEIDENTIFIER    NOT NULL,
        [UpdatedOn]                        DATETIME2           NULL,
	    [UpdatedBy]                        UNIQUEIDENTIFIER    NULL,
        CONSTRAINT [PK_PushNotificationRequest]                               PRIMARY KEY ([PushNotificationRequestId]),
        CONSTRAINT [FK_PushNotificationRequest_PushNotificationTemplateId]    FOREIGN KEY ([PushNotificationTemplateId]) REFERENCES [ns].[PushNotificationTemplate]([PushNotificationTemplateId])
    );

    CREATE NONCLUSTERED INDEX [IX_PushNotificationRequest_TenantId] ON [ns].[PushNotificationRequest]
    (
        [TenantId] ASC
    );
END
GO

IF [dbo].[SecurityPolicyExists]('PushNotificationRequestAccessPolicy') = 0
BEGIN
    -- If the security policy does not exist, create it
    PRINT 'Creating security policy: PushNotificationRequestAccessPolicy';
    CREATE SECURITY POLICY [ns].[PushNotificationRequestAccessPolicy]
		ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[PushNotificationRequest],
        ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[PushNotificationRequest]
        WITH (STATE = ON);
END
GO