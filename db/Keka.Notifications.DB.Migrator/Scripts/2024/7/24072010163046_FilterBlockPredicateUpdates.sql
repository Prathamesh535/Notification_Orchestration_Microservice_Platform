IF [dbo].[SecurityPolicyExists]('EmailRequestAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[EmailRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmailRequestAccessPolicy]
END
GO

IF [dbo].[SecurityPolicyExists]('EmailTemplateAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[EmailTemplateAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmailTemplateAccessPolicy]
END
GO

IF [dbo].[SecurityPolicyExists]('EmailAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[EmailAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmailAccessPolicy]
END
GO

IF [dbo].[SecurityPolicyExists]('EmailStatusAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[EmailStatusAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmailStatusAccessPolicy]
END
GO

IF [dbo].[SecurityPolicyExists]('EmployeeNotificationPreferenceAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[EmployeeNotificationPreferenceAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmployeeNotificationPreferenceAccessPolicy]
END
GO

IF [dbo].[SecurityPolicyExists]('InAppNotificationRequestAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[InAppNotificationRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[InAppNotificationRequestAccessPolicy]
END
GO

IF [dbo].[SecurityPolicyExists]('PushNotificationRequestAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[PushNotificationRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[PushNotificationRequestAccessPolicy]
END
GO

IF [dbo].[SecurityPolicyExists]('SmsRequestAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[SmsRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[SmsRequestAccessPolicy]
END
GO

IF [dbo].[SecurityPolicyExists]('WaMessageRequestAccessPolicy') != 0
BEGIN
	PRINT 'Dropping security policy: [ns].[WaMessageRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[WaMessageRequestAccessPolicy]
END
GO

PRINT 'Altering function: [ns].[fn_TenantAccessPredicate]';
BEGIN
EXEC('ALTER FUNCTION [ns].[fn_TenantAccessPredicate] (@TenantId UNIQUEIDENTIFIER, @IsDeleted BIT)
            RETURNS TABLE
            WITH SCHEMABINDING
         AS
            RETURN SELECT 1 AS result 
            WHERE (@TenantId = CAST(SESSION_CONTEXT(N''TenantId'') AS UNIQUEIDENTIFIER) AND @IsDeleted = 0) OR 
                  CAST(SESSION_CONTEXT(N''IsMigration'') AS INT) = 1;')
END
GO

IF [dbo].[SecurityPolicyExists]('EmailRequestAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[EmailRequest]') != 0
BEGIN
	PRINT 'Adding security policy: [ns].[EmailRequestAccessPolicy]';

	CREATE SECURITY POLICY [ns].[EmailRequestAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[EmailRequest],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[EmailRequest]
	WITH (STATE = ON);
END
GO

IF [dbo].[SecurityPolicyExists]('EmailAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[Email]') != 0
BEGIN
	PRINT 'Adding security policy: [ns].[EmailAccessPolicy]';

	CREATE SECURITY POLICY [ns].[EmailAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[Email],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[Email]
	WITH (STATE = ON);
END
GO

IF [dbo].[SecurityPolicyExists]('EmailStatusAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[EmailStatus]') != 0
BEGIN
	PRINT 'Adding security policy: [ns].[EmailStatusAccessPolicy]';

	CREATE SECURITY POLICY [ns].[EmailStatusAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[EmailStatus],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[EmailStatus]
	WITH (STATE = ON);
END
GO

IF [dbo].[SecurityPolicyExists]('EmailTemplateAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[EmailTemplate]') != 0
BEGIN
	PRINT 'Adding security policy: [ns].[EmailTemplateAccessPolicy]';

	CREATE SECURITY POLICY [ns].[EmailTemplateAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[EmailTemplate],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[EmailTemplate]
	WITH (STATE = ON);
END
GO

IF [dbo].[SecurityPolicyExists]('EmployeeNotificationPreferenceAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[EmployeeNotificationPreference]') != 0
BEGIN
	PRINT 'Adding security policy: [ns].[EmployeeNotificationPreferenceAccessPolicy]';

	CREATE SECURITY POLICY [ns].[EmployeeNotificationPreferenceAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[EmployeeNotificationPreference],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[EmployeeNotificationPreference]
	WITH (STATE = ON);
END
GO

IF [dbo].[SecurityPolicyExists]('InAppNotificationRequestAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[InAppNotificationRequest]') != 0
BEGIN
	PRINT 'Adding security policy: [ns].[InAppNotificationRequestAccessPolicy]';

	CREATE SECURITY POLICY [ns].[InAppNotificationRequestAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[InAppNotificationRequest],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[InAppNotificationRequest]
	WITH (STATE = ON);
END
GO

IF [dbo].[SecurityPolicyExists]('PushNotificationRequestAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[PushNotificationRequest]') != 0
BEGIN
	PRINT 'Adding security policy: [ns].[PushNotificationRequestAccessPolicy]';

	CREATE SECURITY POLICY [ns].[PushNotificationRequestAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[PushNotificationRequest],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[PushNotificationRequest]
	WITH (STATE = ON);
END
GO

IF [dbo].[SecurityPolicyExists]('SmsRequestAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[SmsRequest]') != 0
BEGIN
	PRINT 'Adding security policy: [ns].[SmsRequestAccessPolicy]';

	CREATE SECURITY POLICY [ns].[SmsRequestAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[SmsRequest],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[SmsRequest]
	WITH (STATE = ON);
END
GO

IF [dbo].[SecurityPolicyExists]('WaMessageRequestAccessPolicy') = 0 AND [dbo].[TableExists]('[ns].[WaMessageRequest]') != 0
BEGIN
	CREATE SECURITY POLICY [ns].[WaMessageRequestAccessPolicy]
	ADD FILTER PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, IsDeleted) ON [ns].[WaMessageRequest],
	ADD BLOCK PREDICATE [ns].[fn_TenantAccessPredicate](TenantId, 0) ON [ns].[WaMessageRequest]
	WITH (STATE = ON);
END
GO

IF [dbo].[IndexExists]('ns.WaMessageRequest', 'IX_WaMessageRequest_TenantId') = 0
BEGIN
	CREATE NONCLUSTERED INDEX [IX_WaMessageRequest_TenantId] ON [ns].[WaMessageRequest]
	(
		[TenantId] ASC
	) WHERE IsDeleted = 0;
END
GO