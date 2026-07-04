IF [dbo].[IndexExists]('ns.EmailRequest', 'IX_EmailRequest_TenantId') = 1
BEGIN
	PRINT 'Dropping index [IX_EmailRequest_TenantId]';
	DROP INDEX [ns].[EmailRequest].IX_EmailRequest_TenantId;
END
GO
 
IF [dbo].[IndexExists]('ns.Email', 'IX_Email_TenantId') = 1
BEGIN
	PRINT 'Dropping index [IX_Email_TenantId]';
	DROP INDEX [ns].[Email].IX_Email_TenantId;
END
GO
 
IF [dbo].[SecurityPolicyExists]('EmailRequestAccessPolicy') = 1
BEGIN
	PRINT 'Dropping security policy: [ns].[EmailRequestAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmailRequestAccessPolicy]
END
GO
 
IF [dbo].[SecurityPolicyExists]('EmailAccessPolicy') = 1
BEGIN
	PRINT 'Dropping security policy: [ns].[EmailAccessPolicy]';
	DROP SECURITY POLICY [ns].[EmailAccessPolicy]
END
GO