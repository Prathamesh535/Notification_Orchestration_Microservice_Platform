CREATE TABLE [ns].[Module]
(
    [ModuleId]      UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_Module_ModuleId DEFAULT NEWSEQUENTIALID(),
    [Name]          NVARCHAR(100)       NOT NULL,
    [Description]   NVARCHAR(255)       NULL,
    [CreatedOn]     DATETIME2           NOT NULL    CONSTRAINT DF_Module_CreatedOn DEFAULT GETUTCDATE(),
    [CreatedBy]     UNIQUEIDENTIFIER    NOT NULL    
    CONSTRAINT [PK_Module] PRIMARY KEY ([ModuleId])
)
GO