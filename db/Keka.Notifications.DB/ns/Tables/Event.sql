CREATE TABLE [ns].[Event]
(
    [EventId]                   UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_Event_EventId DEFAULT NEWSEQUENTIALID(),
    [ModuleId]                  UNIQUEIDENTIFIER    NOT NULL,
    [EventCode]                 NVARCHAR(100)       NULL,
    [Name]                      NVARCHAR(100)       NOT NULL,
    [Description]               NVARCHAR(255)       NULL,
    [NotificationChannels]      VARCHAR(MAX)        NULL,
    [CreatedOn]                 DATETIME2           NOT NULL    CONSTRAINT DF_Event_CreatedOn DEFAULT GETUTCDATE(),
    [CreatedBy]                 UNIQUEIDENTIFIER    NOT NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY ([EventId]),
    CONSTRAINT [FK_Event_ModuleId] FOREIGN KEY (ModuleId) REFERENCES [ns].[Module]([ModuleId])
)
GO