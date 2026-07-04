CREATE TABLE [ns].[Employee]
(
	[EmployeeId]    UNIQUEIDENTIFIER    NOT NULL,
    [TenantId]		UNIQUEIDENTIFIER    NOT NULL, 
	[DisplayName]   NVARCHAR(256)       NOT NULL, 
    [Email]         NVARCHAR(256)       NOT NULL,
	[IsDeleted]	    BIT                 NOT NULL    CONSTRAINT DF_Employee_IsDeleted DEFAULT 0, 
    [DeletedOn]	    BIGINT              NULL,
    [DeletedBy]	    UNIQUEIDENTIFIER    NULL,
    [CreatedOn]	    BIGINT              NOT NULL, 
    [CreatedBy]	    UNIQUEIDENTIFIER    NOT NULL, 
    [UpdatedOn]	    BIGINT              NULL, 
    [UpdatedBy]	    UNIQUEIDENTIFIER    NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY ([EmployeeId]),
)
GO

CREATE NONCLUSTERED INDEX [IX_Employee_TenantId] ON [ns].[Employee]
(
	[TenantId] ASC
) WHERE IsDeleted = 0;
GO