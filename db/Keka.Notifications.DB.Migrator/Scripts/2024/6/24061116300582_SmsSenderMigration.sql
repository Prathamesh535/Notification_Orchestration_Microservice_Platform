IF [dbo].[TableExists]('[ns].[Country]') = 0
BEGIN
    PRINT 'Creating table [ns].[Country]'
    CREATE TABLE [ns].[Country]
    (
        [CountryId]         UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_Country_CountryId DEFAULT NEWSEQUENTIALID(),
        [Name]              NVARCHAR(256)       NOT NULL,
        [CountryCode]       NVARCHAR(10)        NOT NULL,
        [FromPhoneNumber]   NVARCHAR(20)        NOT NULL,
        [IsSmsEnabled]      BIT                 NOT NULL    CONSTRAINT DF_Country_IsSmsEnabled DEFAULT 0,
        [IsWaEnabled]       BIT                 NOT NULL    CONSTRAINT DF_Country_IsWaEnabled DEFAULT 0,
        [CreatedOn]         DATETIME2           NOT NULL    CONSTRAINT DF_Country_CreatedOn DEFAULT GETUTCDATE(),
        [CreatedBy]         UNIQUEIDENTIFIER    NOT NULL,
        [UpdatedOn]         DATETIME2           NULL,
        [UpdatedBy]         UNIQUEIDENTIFIER    NULL,
        CONSTRAINT [PK_Country] PRIMARY KEY ([CountryId])
    )
    PRINT 'Table [ns].[Country] created successfully.'
END
ELSE
BEGIN
    PRINT 'Table [ns].[Country] already exists.'
END
GO

IF [dbo].[TableExists]('[ns].[SmsTemplate]') = 0
BEGIN
    PRINT 'Creating table [ns].[SmsTemplate]'
    CREATE TABLE [ns].[SmsTemplate]
    (
        [SmsTemplateId]      UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_SmsTemplate_SmsTemplateId DEFAULT NEWSEQUENTIALID(),
        [Name]               NVARCHAR(20)        NOT NULL,
        [ExternalTemplateId] NVARCHAR(64)  		 NOT NULL,
        [ParameterMapping]   NVARCHAR(MAX)       NULL,
        [CreatedOn]          DATETIME2           NOT NULL    CONSTRAINT DF_SmsTemplate_CreatedOn DEFAULT GETUTCDATE(),
        [CreatedBy]          UNIQUEIDENTIFIER    NOT NULL,
        [UpdatedOn]          DATETIME2           NULL,
        [UpdatedBy]          UNIQUEIDENTIFIER    NULL,
        CONSTRAINT [PK_SmsTemplate] PRIMARY KEY ([SmsTemplateId])
    )
    PRINT 'Table [ns].[SmsTemplate] created successfully.'
END
ELSE
BEGIN
    PRINT 'Table [ns].[SmsTemplate] already exists.'
END
GO

IF [dbo].[TableExists]('[ns].[SmsRequest]') = 0
BEGIN
    PRINT 'Creating table [ns].[SmsRequest]'
    CREATE TABLE [ns].[SmsRequest]
    (
        [SmsRequestId]      UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_SmsRequest_SmsRequestId DEFAULT NEWSEQUENTIALID(),
        [TenantId]          UNIQUEIDENTIFIER    NOT NULL,
        [SmsTemplateId]     UNIQUEIDENTIFIER    NOT NULL,
        [Personalization]   NVARCHAR(MAX)       NOT NULL,
        [ExternalId]        NVARCHAR(64)        NULL,
        [Status]            SMALLINT            NOT NULL    CONSTRAINT DF_SmsRequest_Status DEFAULT 0,
        [FailureReason]	    NVARCHAR(500)		NULL,
        [CreatedOn]         DATETIME2           NOT NULL    CONSTRAINT DF_SmsRequest_CreatedOn DEFAULT GETUTCDATE(),
        [CreatedBy]         UNIQUEIDENTIFIER    NOT NULL,
        [UpdatedOn]         DATETIME2           NULL,
        [UpdatedBy]         UNIQUEIDENTIFIER    NULL,
        CONSTRAINT [PK_SmsRequest] PRIMARY KEY ([SmsRequestId])
    )
    PRINT 'Table [ns].[SmsRequest] created successfully.'

    PRINT 'Creating index IX_SmsRequest_TenantId on table [ns].[SmsRequest]'
    CREATE NONCLUSTERED INDEX [IX_SmsRequest_TenantId] ON [ns].[SmsRequest]
    (
        [TenantId] ASC
    )
    PRINT 'Index IX_SmsRequest_TenantId created successfully.'
END
ELSE
BEGIN
    PRINT 'Table [ns].[SmsRequest] already exists.'
END
GO

IF [dbo].[SecurityPolicyExists]('SmsRequestAccessPolicy') = 0
BEGIN
    PRINT 'Creating security policy [ns].[SmsRequestAccessPolicy]'
    CREATE SECURITY POLICY [ns].[SmsRequestAccessPolicy]
        ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[SmsRequest],
        ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[SmsRequest]
        WITH (STATE = ON);
    PRINT 'Security policy [ns].[SmsRequestAccessPolicy] created successfully.'
END
GO