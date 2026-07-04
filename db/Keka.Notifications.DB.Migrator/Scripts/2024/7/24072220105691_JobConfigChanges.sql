IF [dbo].[ConstraintExists]('ns.JobConfig', 'FK_JobConfig_Tenant') = 1
BEGIN
	PRINT 'Dropping constraint: [ns].[FK_JobConfig_Tenant]';
	ALTER TABLE [ns].[JobConfig] DROP CONSTRAINT FK_JobConfig_Tenant;
END
GO

IF [dbo].[TableExists]('[ns].[JobConfig]') = 1
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [ns].[JobConfig] NOLOCK WHERE JobType = 'UnblockEmailJob')
    BEGIN
        PRINT 'Inserting UnblockEmailJob record into table [ns].[JobConfig]'
        INSERT INTO 
            ns.JobConfig(JobConfigId,TenantId,JobType,CronExpression,IsEnabled,CreatedOn,CreatedBy)
        VALUES 
            ('A5641231-3E1F-EF11-8EB9-1CA0B87426C9','00000000-0000-0000-0000-000000000000','UnblockEmailJob','0 0 * * *',1,'1721657988','6030EAB8-BD29-4563-9D07-1D0C2F439745');
    END
END
GO
