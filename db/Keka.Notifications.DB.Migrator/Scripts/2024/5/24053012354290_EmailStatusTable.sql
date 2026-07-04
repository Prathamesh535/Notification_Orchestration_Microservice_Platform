IF [dbo].[TableExists]('[ns].[EmailStatus]') = 0
BEGIN
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
	[IsBlocked]						BIT					NOT NULL	CONSTRAINT DF_EmailStatus_IsBlocked DEFAULT 0,
	[BlockedUntil]					DATETIME2			NULL,	
    [CreatedOn]						DATETIME2           NOT NULL,
	[CreatedBy]						NVARCHAR(256)		NULL,
    [UpdatedOn]						DATETIME2           NULL,
	[UpdatedBy]						NVARCHAR(256)		NULL,
	[IsDeleted]						BIT                 NOT NULL    CONSTRAINT DF_EmailStatus_IsDeleted DEFAULT 0, 
	[DeletedOn]						DATETIME2           NULL
	CONSTRAINT [PK_EmailStatus] PRIMARY KEY ([EmailStatusId]),
)
END
GO

IF [dbo].[IndexExists]('EmailStatus', 'IX_EmailStatus_Email') = 0
BEGIN
CREATE NONCLUSTERED INDEX [IX_EmailStatus_Email] ON [ns].[EmailStatus]
(
	[Email] ASC
) 
INCLUDE ([TenantId])
WHERE [IsDeleted] = 0;
END
GO

IF [dbo].[TableExists]('[ns].[Employee]') != 0 AND dbo.ColumnExists('[ns].[Employee]', 'Email') = 0
BEGIN
	PRINT 'Adding Email column to the Employee table.';	
	ALTER TABLE [ns].[Employee] ADD [Email] NVARCHAR(256) NULL
END
GO