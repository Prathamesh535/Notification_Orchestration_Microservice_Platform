CREATE TABLE [ns].[Country]
(
    [CountryId]         UNIQUEIDENTIFIER    NOT NULL    CONSTRAINT DF_Country_CountryId DEFAULT NEWSEQUENTIALID(),
    [Name]              NVARCHAR(256)       NOT NULL,
    [CountryCode]       NVARCHAR(10)        NOT NULL,
    [FromPhoneNumber]   NVARCHAR(20)        NOT NULL,
    [IsSmsEnabled]      BIT                 NOT NULL    CONSTRAINT DF_Country_IsSmsEnabled DEFAULT 0,
    [IsWaEnabled]       BIT                 NOT NULL    CONSTRAINT DF_Country_IsWaEnabled DEFAULT 0,
    [CreatedOn]         DATETIME2           NOT NULL    CONSTRAINT DF_Country_CreatedOn DEFAULT GETUTCDATE(),
    [CreatedBy]         UNIQUEIDENTIFIER    NOT NULL,
    [UpdatedOn]         DATETIME2           NULL,
    [UpdatedBy]         UNIQUEIDENTIFIER    NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([CountryId])
)
GO
