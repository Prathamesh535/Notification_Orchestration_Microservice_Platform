CREATE TABLE [ns].[Template]
(
	[TemplateId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialId(), 
    [TenantId] UNIQUEIDENTIFIER  NOT NULL,
    [TemplateType] SMALLINT NOT NULL,
	[Name] NVARCHAR(128) NOT NULL,
	[Subject] NVARCHAR(MAX) NULL,
	[Body] NVARCHAR(MAX) NOT NULL,
	[IsEnabled] BIT NOT NULL CONSTRAINT [DF_Template_IsEnabled] DEFAULT 1,
    [IsDeleted] BIT NOT NULL CONSTRAINT [DF_Template_IsDeleted] DEFAULT 0,
    [DeletedBy] UNIQUEIDENTIFIER NULL, 
    [DeletedOn] DATETIME2 NULL, 
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn] DATETIME2 NOT NULL CONSTRAINT [DF_Template_CreatedOn] DEFAULT GetUtcDate(), 
    [UpdatedOn] DATETIME2  NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_TemplateId] PRIMARY KEY CLUSTERED ([TemplateId] ASC),
	CONSTRAINT [FK_Template_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [ns].[Tenant]([TenantId])
)
GO

CREATE NONCLUSTERED INDEX [Idx_Template]
    ON [ns].[Template]([TenantId] ASC, IsDeleted DESC);
GO

CREATE SECURITY POLICY [ns].[TemplateAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[Template],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId) ON [ns].[Template]
	WITH (STATE = ON);
GO