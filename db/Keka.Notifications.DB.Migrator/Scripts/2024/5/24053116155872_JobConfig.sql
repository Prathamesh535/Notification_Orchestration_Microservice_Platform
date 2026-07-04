IF [dbo].[TableExists]('[ns].[JobConfig]') = 0
BEGIN
CREATE TABLE [ns].[JobConfig]
(
	[JobConfigId]		UNIQUEIDENTIFIER			NOT NULL DEFAULT NEWSEQUENTIALID(),
	[TenantId]			UNIQUEIDENTIFIER			NOT NULL,
	[JobType]			NVARCHAR(256)				NOT NULL,
	[CronExpression]	NVARCHAR(64)				NOT NULL,
	[IsEnabled]			BIT							NOT NULL	CONSTRAINT [DF_JobConfig_IsEnabled] DEFAULT 1,
	[IsDeleted]         BIT							NOT NULL	CONSTRAINT [DF_JobConfig_IsDeleted] DEFAULT 0,
    [DeletedOn]         BIGINT						NULL,
    [DeletedBy]         UNIQUEIDENTIFIER			NULL,
    [CreatedOn]         BIGINT						NOT NULL,
    [CreatedBy]         UNIQUEIDENTIFIER			NOT NULL,
    [UpdatedOn]         BIGINT						NULL,
    [UpdatedBy]         UNIQUEIDENTIFIER			NULL,
	CONSTRAINT [PK_JobConfig] PRIMARY KEY CLUSTERED ([JobConfigId] ASC),
	CONSTRAINT [FK_JobConfig_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [ns].[Tenant]([TenantId]),
);
END
GO

IF [dbo].[IndexExists]('JobConfig', 'IX_JobConfig_Tenant') = 0
BEGIN
CREATE NONCLUSTERED INDEX [IX_JobConfig_Tenant]
ON [ns].[JobConfig]([TenantId] ASC, [JobType] ASC)
INCLUDE([IsEnabled]);
END
GO