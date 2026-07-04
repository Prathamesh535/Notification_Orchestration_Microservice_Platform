CREATE FUNCTION [ns].[fn_TenantAccessPredicate] (@TenantId UNIQUEIDENTIFIER, @IsDeleted BIT)
    RETURNS TABLE
    WITH SCHEMABINDING
AS
    RETURN SELECT 1 AS result 
    WHERE (@TenantId = CAST(SESSION_CONTEXT(N'TenantId') AS UNIQUEIDENTIFIER) AND @IsDeleted = 0) OR 
            CAST(SESSION_CONTEXT(N'IsMigration') AS INT) = 1;
GO