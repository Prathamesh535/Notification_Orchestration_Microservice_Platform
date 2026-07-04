IF [dbo].[TableExists]('[ns].[Country]') = 1
BEGIN
    PRINT 'Checking for existing record in table [ns].[Country]'
    IF NOT EXISTS (SELECT 1 FROM [ns].[Country] WHERE [CountryId] = 'E02F46F2-D023-EF11-8EBB-1CA0B87426C9')
    BEGIN
        PRINT 'Inserting default data into table [ns].[Country]'
        INSERT INTO [ns].[Country] ([CountryId], [Name], [CountryCode], [FromPhoneNumber], [IsSmsEnabled], [IsWaEnabled], [CreatedOn], [CreatedBy])
        VALUES 
            ('E02F46F2-D023-EF11-8EBB-1CA0B87426C9', 'India', '91', '918688827939', 1, 1, GETUTCDATE(), '5D9218AA-EC71-4910-BE81-14C721E15B7F')
        PRINT 'Default data inserted into table [ns].[Country]'
    END
    ELSE
    BEGIN
        PRINT 'Record already exists in table [ns].[Country]'
    END
END
ELSE
BEGIN
    PRINT 'Table [ns].[Country] does not exist.'
END
GO