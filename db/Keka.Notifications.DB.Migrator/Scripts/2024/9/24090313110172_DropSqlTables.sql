IF [dbo].[SecurityPolicyExists]('EmailRequestAccessPolicy') = 1
BEGIN
	PRINT 'Dropping security policy: [ns].[EmailRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmailRequestAccessPolicy]
END
GO

IF [dbo].[TableExists]('[ns].[EmailRequest]') = 1
BEGIN
    PRINT 'Droping table: [ns].[EmailRequest]';
	DROP TABLE [ns].[EmailRequest]
END
GO

IF [dbo].[SecurityPolicyExists]('EmailAccessPolicy') = 1
BEGIN
	PRINT 'Dropping security policy: [ns].[EmailAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmailAccessPolicy]
END
GO

IF [dbo].[TableExists]('[ns].[Email]') = 1
BEGIN
    PRINT 'Droping table: [ns].[Email]';
	DROP TABLE [ns].[Email]
END
GO

IF [dbo].[SecurityPolicyExists]('WaMessageRequestAccessPolicy') = 1
BEGIN
	PRINT 'Dropping security policy: [ns].[WaMessageRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[WaMessageRequestAccessPolicy]
END
GO

IF [dbo].[TableExists]('[ns].[WaMessageRequest]') = 1
BEGIN
    PRINT 'Droping table: [ns].[WaMessageRequest]';
	DROP TABLE [ns].[WaMessageRequest]
END
GO

IF [dbo].[TableExists]('[ns].[WaTemplate]') = 1
BEGIN
    PRINT 'Droping table: [ns].[WaTemplate]';
	DROP TABLE [ns].[WaTemplate]
END
GO

IF [dbo].[SecurityPolicyExists]('SmsRequestAccessPolicy') = 1
BEGIN
	PRINT 'Dropping security policy: [ns].[SmsRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[SmsRequestAccessPolicy]
END
GO

IF [dbo].[TableExists]('[ns].[SmsRequest]') = 1
BEGIN
    PRINT 'Droping table: [ns].[SmsRequest]';
	DROP TABLE [ns].[SmsRequest]
END
GO

IF [dbo].[TableExists]('[ns].[SmsTemplate]') = 1
BEGIN
    PRINT 'Droping table: [ns].[SmsTemplate]';
	DROP TABLE [ns].[SmsTemplate]
END
GO

IF [dbo].[SecurityPolicyExists]('PushNotificationRequestAccessPolicy') = 1
BEGIN
	PRINT 'Dropping security policy: [ns].[PushNotificationRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[PushNotificationRequestAccessPolicy]
END
GO

IF [dbo].[TableExists]('[ns].[PushNotificationRequest]') = 1 
BEGIN
	PRINT 'Droping table: [ns].[PushNotificationRequest]';
	DROP TABLE [ns].[PushNotificationRequest]
END
GO

IF [dbo].[TableExists]('[ns].[PushNotificationTemplate]') = 1 
BEGIN
	PRINT 'Droping table: [ns].[PushNotificationTemplate]';
	DROP TABLE [ns].[PushNotificationTemplate]
END
GO

IF [dbo].[SecurityPolicyExists]('InAppNotificationRequestAccessPolicy') = 1
BEGIN
	PRINT 'Dropping security policy: [ns].[InAppNotificationRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[InAppNotificationRequestAccessPolicy]
END
GO

IF [dbo].[TableExists]('[ns].[InAppNotificationRequest]') = 1 
BEGIN
	PRINT 'Droping table: [ns].[InAppNotificationRequest]';
	DROP TABLE [ns].[InAppNotificationRequest]
END
GO

IF [dbo].[TableExists]('[ns].[InAppNotificationTemplate]') = 1 
BEGIN
	PRINT 'Droping table: [ns].[InAppNotificationTemplate]';
	DROP TABLE [ns].[InAppNotificationTemplate]
END
GO

IF [dbo].[TableExists]('[ns].[SlackNotificationTemplate]') = 1
BEGIN
    PRINT 'Droping table: [ns].[SlackNotificationTemplate]';
	DROP TABLE [ns].[SlackNotificationTemplate]
END
GO