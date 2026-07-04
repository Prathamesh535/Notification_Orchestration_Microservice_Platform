CREATE TABLE [ns].[Tenant]
(
	[TenantId]      UNIQUEIDENTIFIER    NOT NULL,
	[Name]          NVARCHAR(256)       NOT NULL, 
	[IsDeleted]	    BIT                 NOT NULL    CONSTRAINT DF_Tenant_IsDeleted DEFAULT 0, 
    [DeletedOn]	    BIGINT              NULL,
    [DeletedBy]	    UNIQUEIDENTIFIER    NULL,
    [CreatedOn]	    BIGINT              NOT NULL, 
    [CreatedBy]	    UNIQUEIDENTIFIER    NOT NULL, 
    [UpdatedOn]	    BIGINT              NULL, 
    [UpdatedBy]	    UNIQUEIDENTIFIER    NULL,
    CONSTRAINT [PK_Tenant] PRIMARY KEY ([TenantId])
)
GO