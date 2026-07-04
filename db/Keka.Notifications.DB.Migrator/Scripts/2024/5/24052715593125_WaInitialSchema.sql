IF [dbo].[TableExists]('[ns].[WaTemplate]') = 0
BEGIN
CREATE TABLE [ns].[WaTemplate]
(
	[WaTemplateId]		   UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_WaTemplate_WaTemplateId DEFAULT NEWSEQUENTIALID(),
	[Name]                 NVARCHAR(50)		   NOT NULL,
	[Language]             NVARCHAR(10)		   NOT NULL,
	[ExternalTemplateId]   NVARCHAR(64)        NOT NULL,
	[ParameterMapping]	   NVARCHAR(MAX)       NULL,
	[CreatedOn]            Datetime2           NOT NULL	   CONSTRAINT DF_WaTemplate_CreatedOn DEFAULT GETUTCDATE(),
    [CreatedBy]	           UNIQUEIDENTIFIER    NOT NULL,
    [UpdatedOn]            Datetime2           NULL,
    [UpdatedBy]	           UNIQUEIDENTIFIER    NULL,
	CONSTRAINT [PK_WaTemplate] PRIMARY KEY ([WaTemplateId]),
)
END
GO

IF [dbo].[TableExists]('[ns].[WaMessageRequest]') = 0
BEGIN
CREATE TABLE [ns].[WaMessageRequest]
(
	[WaMessageRequestId]	UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_WaMessageRequest_WaMessageRequestId DEFAULT NEWSEQUENTIALID(),
	[TenantId]				UNIQUEIDENTIFIER    NOT NULL,
	[WaTemplateId]			UNIQUEIDENTIFIER	NOT NULL,
	[Personalization]		NVARCHAR(MAX)       NOT NULL,
	[Status]				SMALLINT			NOT NULL	CONSTRAINT DF_WaMessageRequest_Status DEFAULT 0,
	[ExternalId]			VARCHAR(64)			NULL,
	[FailureReason]			NVARCHAR(500)		NULL,
	[CreatedOn]				Datetime2           NOT NULL	CONSTRAINT DF_WaMessageRequest_CreatedOn DEFAULT GETUTCDATE(),
    [CreatedBy]				UNIQUEIDENTIFIER    NOT NULL,
    [UpdatedOn]				Datetime2           NULL,
    [UpdatedBy]				UNIQUEIDENTIFIER    NULL,
	CONSTRAINT [PK_WaMessageRequest] PRIMARY KEY ([WaMessageRequestId]),
	CONSTRAINT [FK_WaMessageRequest_WaTemplateId] FOREIGN KEY (WaTemplateId) REFERENCES [ns].[WaTemplate] (WaTemplateId),
)
END
GO

IF [dbo].[SecurityPolicyExists]('WaMessageRequestAccessPolicy') = 0
BEGIN
CREATE SECURITY POLICY [ns].[WaMessageRequestAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[WaMessageRequest],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[WaMessageRequest]
	WITH (STATE = ON);
END
GO