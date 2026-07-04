IF [dbo].[SchemaExists]('ns') = 0
BEGIN
	EXEC ('CREATE SCHEMA [ns] AUTHORIZATION [dbo]')
END
GO

IF [dbo].[FunctionExists]('[ns].[fn_TenantAccessPredicate]') = 0
BEGIN
    -- If the function does not exist, create it
    PRINT 'Creating function: [ns].[fn_TenantAccessPredicate]';
    EXEC('
        CREATE FUNCTION [ns].[fn_TenantAccessPredicate] (@TenantId UNIQUEIDENTIFIER)
            RETURNS TABLE
            WITH SCHEMABINDING
         AS
            RETURN SELECT 1 AS result 
            WHERE @TenantId = CAST(SESSION_CONTEXT(N''TenantId'') AS UNIQUEIDENTIFIER);
    ');
END
GO

IF [dbo].[TableExists]('[ns].[Tenant]') = 0
BEGIN
	-- If the table does not exist, create it
	PRINT 'Creating table: [ns].[Tenant]';
	CREATE TABLE [ns].[Tenant]
	(
		[TenantId] UNIQUEIDENTIFIER NOT NULL,
		[Name]		NVARCHAR(256) NOT NULL, 
		[IsDeleted]			BIT                 NOT NULL    CONSTRAINT DF_Tenant_IsDeleted DEFAULT 0, 
		[DeletedOn]			BIGINT              NULL,
		[DeletedBy]			UNIQUEIDENTIFIER    NULL,
		[CreatedOn]			BIGINT              NOT NULL, 
		[CreatedBy]			UNIQUEIDENTIFIER    NOT NULL, 
		[UpdatedOn]			BIGINT              NULL, 
		[UpdatedBy]			UNIQUEIDENTIFIER    NULL,
		CONSTRAINT [PK_Tenant] PRIMARY KEY ([TenantId])
	)
END
GO

IF [dbo].[TableExists]('[ns].[Employee]') = 0
BEGIN
	-- If the table does not exist, create it
	PRINT 'Creating table: [ns].[Employee]';
	CREATE TABLE [ns].[Employee]
	(
		[EmployeeId]    UNIQUEIDENTIFIER    NOT NULL,
		[TenantId]		UNIQUEIDENTIFIER    NOT NULL, 
		[DisplayName]   NVARCHAR(256)       NOT NULL, 
		[IsDeleted]	    BIT                 NOT NULL    CONSTRAINT DF_Employee_IsDeleted DEFAULT 0, 
		[DeletedOn]	    BIGINT              NULL,
		[DeletedBy]	    UNIQUEIDENTIFIER    NULL,
		[CreatedOn]	    BIGINT              NOT NULL, 
		[CreatedBy]	    UNIQUEIDENTIFIER    NOT NULL, 
		[UpdatedOn]	    BIGINT              NULL, 
		[UpdatedBy]	    UNIQUEIDENTIFIER    NULL,
		CONSTRAINT [PK_Employee] PRIMARY KEY ([EmployeeId])
	)

	CREATE NONCLUSTERED INDEX [IX_Employee_TenantId] ON [ns].[Employee]
	(
		[TenantId] ASC
	) WHERE IsDeleted = 0;
END
GO

IF [dbo].[TableExists]('[ns].[EmailRequest]') = 0
BEGIN
	-- If the table does not exist, create it
	PRINT 'Creating table: [ns].[EmailRequest]';
    CREATE TABLE [ns].[EmailRequest]
    (
        [EmailRequestId]    UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_EmailRequest_EmailRequestId DEFAULT NEWSEQUENTIALID(),
        [TenantId]          UNIQUEIDENTIFIER    NOT NULL,
        [From]              NVARCHAR(2048)      NOT NULL,
        [Personalization]   NVARCHAR(MAX)       NOT NULL,
        [ReplyTo]           NVARCHAR(2048)      NULL,
        [Content]           NVARCHAR(MAX)       NOT NULL,
        [Attachments]       NVARCHAR(MAX)       NULL,
        [TemplateId]        UNIQUEIDENTIFIER    NULL,
        [SendAt]            DATETIME2           NULL,
        [TrackingSettings]  NVARCHAR(MAX)       NULL,
        [BatchId]           UNIQUEIDENTIFIER    NULL,
        [IsDeleted]	        BIT                 NOT NULL    CONSTRAINT DF_EmailRequest_IsDeleted DEFAULT 0,
        [DeletedOn]	        DATETIME2           NULL,
        [DeletedBy]	        UNIQUEIDENTIFIER    NULL,
        [CreatedOn]	        DATETIME2           NOT NULL    CONSTRAINT DF_EmailRequest_CreatedOn DEFAULT GETUTCDATE(),
        [CreatedBy]	        UNIQUEIDENTIFIER    NOT NULL,
        [UpdatedOn]	        DATETIME2           NULL,
        [UpdatedBy]	        UNIQUEIDENTIFIER    NULL,
        CONSTRAINT [PK_EmailRequest] PRIMARY KEY ([EmailRequestId])
    )

    CREATE NONCLUSTERED INDEX [IX_EmailRequest_TenantId] ON [ns].[EmailRequest]
    (
        [TenantId] ASC, [IsDeleted] ASC
    )
END
GO

IF [dbo].[SecurityPolicyExists]('EmailRequestAccessPolicy') = 0
BEGIN
    -- If the security policy does not exist, create it
    PRINT 'Creating security policy: EmailRequestAccessPolicy';
    CREATE SECURITY POLICY [ns].[EmailRequestAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[EmailRequest],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[EmailRequest]
	WITH (STATE = ON);
END
GO

IF [dbo].[TableExists]('[ns].[Email]') = 0
BEGIN
	-- If the table does not exist, create it
	PRINT 'Creating table: [ns].[Email]';
    CREATE TABLE [ns].[Email]
    (
        [EmailId]    UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_Email_EmailId DEFAULT NEWSEQUENTIALID(),
        [From]              NVARCHAR(2048)      NOT NULL,
        [Recipients]        NVARCHAR(MAX)       NOT NULL,
        [Content]           NVARCHAR(MAX)       NOT NULL,
        [ReplyTo]           NVARCHAR(2048)      NULL,
        [Attachments]       NVARCHAR(MAX)       NULL,
        [TenantId]          UNIQUEIDENTIFIER    NOT NULL,
        [IsDeleted]	        BIT                 NOT NULL    CONSTRAINT DF_Email_IsDeleted DEFAULT 0,
        [DeletedOn]         Datetime2              NULL,
        [DeletedBy]	        UNIQUEIDENTIFIER    NULL,
        [CreatedOn]         Datetime2              NOT NULL,
        [CreatedBy]	        UNIQUEIDENTIFIER    NOT NULL,
        [UpdatedOn]         Datetime2              NULL,
        [UpdatedBy]	        UNIQUEIDENTIFIER    NULL,
        CONSTRAINT [PK_Email] PRIMARY KEY ([EmailId]),
        CONSTRAINT [FK_Email_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [ns].[Tenant]([TenantId])
    )

    CREATE NONCLUSTERED INDEX [IX_Email_TenantId] ON [ns].[Email]
    (
            [TenantId] ASC
    ) WHERE IsDeleted = 0;
    END
GO

IF [dbo].[SecurityPolicyExists]('EmailAccessPolicy') = 0
BEGIN
    -- If the security policy does not exist, create it
    PRINT 'Creating security policy: EmailAccessPolicy';
    CREATE SECURITY POLICY [ns].[EmailAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[Email],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[Email]
	WITH (STATE = ON);
END
GO

IF [dbo].[TableExists]('[ns].[EmailTemplate]') = 0
BEGIN
        CREATE TABLE [ns].[EmailTemplate]
        (
            [EmailTemplateId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_EmailTemplate_EmailTemplateId DEFAULT NEWSEQUENTIALID(),
            [TenantId] UNIQUEIDENTIFIER  NOT NULL,
            [Name] NVARCHAR(128) NOT NULL,
            [Subject] NVARCHAR(MAX) NOT NULL,
            [Body] NVARCHAR(MAX) NOT NULL,
            [IsDisabled] BIT NOT NULL CONSTRAINT [DF_EmailTemplate_IsDisabled] DEFAULT 1,
            [IsDeleted] BIT NOT NULL CONSTRAINT [DF_EmailTemplate_IsDeleted] DEFAULT 0,
            [DeletedBy] UNIQUEIDENTIFIER NULL,
            [DeletedOn] DATETIME2 NULL,
            [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
            [CreatedOn] DATETIME2 NOT NULL CONSTRAINT [DF_EmailTemplate_CreatedOn] DEFAULT GetUtcDate(),
            [UpdatedOn] DATETIME2  NULL,
            [UpdatedBy] UNIQUEIDENTIFIER NULL,
            CONSTRAINT [PK_EmailTemplateId] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC)
            )

        CREATE NONCLUSTERED INDEX [IX_EmailTemplate_TenantId] ON [ns].[EmailTemplate]
         ([TenantId] ASC, IsDeleted ASC);
END
GO

IF [dbo].[SecurityPolicyExists]('EmailAccessPolicy') = 0
BEGIN
    -- If the security policy does not exist, create it
    PRINT 'Creating security policy: EmailTemplateAccessPolicy';
    CREATE SECURITY POLICY [ns].[EmailTemplateAccessPolicy]
        ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[EmailTemplate],
        ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[EmailTemplate]
        WITH (STATE = ON);
END
GO
