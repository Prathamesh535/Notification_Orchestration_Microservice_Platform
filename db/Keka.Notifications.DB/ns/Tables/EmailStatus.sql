CREATE TABLE [ns].[EmailStatus]
(
	[EmailStatusId]					UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_EmailStatus_EmailStatusId DEFAULT NEWSEQUENTIALID(),
	[EmployeeId]					UNIQUEIDENTIFIER    NULL,
	[TenantId]						UNIQUEIDENTIFIER    NOT NULL,
	[Email]							NVARCHAR(256)		NOT NULL,
	[NoOfEmailsSent]				INT					NOT NULL	CONSTRAINT DF_EmailStatus_NoOfEmailsSent DEFAULT 0,
	[NoOfEmailsFailed]				INT					NOT NULL	CONSTRAINT DF_EmailStatus_NoOfEmailsFailed DEFAULT 0,
	[LastEmailSentOn]				DATETIME2			NULL,
	[LastEmailDeliveredOn]			DATETIME2			NULL,
	[LastEmailFailedOn]				DATETIME2			NULL,
	[LastEmailFailedReason]			TINYINT				NULL,
	[ConsecutiveBlockCount]			TINYINT				NOT NULL	CONSTRAINT DF_EmailStatus_ConsecutiveBlockCount DEFAULT 0,
	[PreviousStateDetails]			NVARCHAR(500)		NULL,
	[IsBlocked]						BIT					NOT NULL	CONSTRAINT DF_EmailStatus_IsBlocked DEFAULT 0,
	[BlockedUntil]					DATETIME2			NULL,
	[LastBlockedOn]                 DATETIME2           NULL,
    [CreatedOn]						DATETIME2           NOT NULL,
	[CreatedBy]						NVARCHAR(256)		NULL,
    [UpdatedOn]						DATETIME2           NULL,
	[UpdatedBy]						NVARCHAR(256)		NULL,
	[IsDeleted]						BIT                 NOT NULL    CONSTRAINT DF_EmailStatus_IsDeleted DEFAULT 0, 
	[DeletedOn]						DATETIME2           NULL,
	[DeletedBy]						UNIQUEIDENTIFIER	NULL,
	CONSTRAINT [PK_EmailStatus] PRIMARY KEY ([EmailStatusId]),
)
GO

CREATE NONCLUSTERED INDEX [IX_EmailStatus_TenantId_Email_IsBlocked] ON [ns].[EmailStatus]
(
	[TenantId] ASC,
	[Email] ASC,
	[IsBlocked] DESC
) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_EmailStatus_TenantId_EmployeeId] ON [ns].[EmailStatus]
(
	[TenantId] ASC,
	[EmployeeId] ASC
) WHERE [IsDeleted] = 0;
GO


CREATE NONCLUSTERED INDEX [IX_EmailStatus_Email] ON [ns].[EmailStatus]
(
	[Email] ASC
)INCLUDE ([TenantId]) 
WHERE ([IsDeleted]=(0))
WITH (ONLINE = ON) ON [PRIMARY]
GO
       
CREATE NONCLUSTERED INDEX [IX_EmailStatus_EMail_IsBlocked] ON [ns].[EmailStatus] 
(   [Email],
    [IsBlocked]
) INCLUDE ([BlockedUntil], [ConsecutiveBlockCount], [EmployeeId], [IsDeleted], [LastBlockedOn],
       [LastEmailDeliveredOn], [LastEmailFailedOn], [LastEmailFailedReason], [LastEmailSentOn],
       [NoOfEmailsFailed], [PreviousStateDetails], [TenantId]) 
WHERE ([IsDeleted]=(0))
WITH (ONLINE = ON) ON [PRIMARY]
GO

CREATE SECURITY POLICY [ns].[EmailStatusAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[EmailStatus],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[EmailStatus]
	WITH (STATE = ON);
GO