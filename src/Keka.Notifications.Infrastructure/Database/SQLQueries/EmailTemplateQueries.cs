// -----------------------------------------------------------------------
// <copyright file="EmailTemplateQueries.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

/// <summary>
/// Represents the template queries.
/// </summary>
internal static class EmailTemplateQueries
{
    /// <summary>
    /// Represents the select templates query.
    /// </summary>
    public const string SelectEmailTemplateById = @"
        SELECT [EmailTemplateId],[Name],[Subject],[Body] 
        FROM [ns].[EmailTemplate] 
        Where EmailTemplateId = @EmailTemplateId AND IsDeleted = 0
    ";

    /// <summary>
    /// The insert template query.
    /// </summary>
    public const string InsertEmailTemplate = @"
        INSERT INTO [ns].[EmailTemplate] (EmailTemplateId, TenantId, Name, Subject, Body, CreatedOn, CreatedBy) 
        OUTPUT INSERTED.EmailTemplateId
        VALUES (DEFAULT, @TenantId, @Name, @Subject, @Body, @CreatedOn, @CreatedBy)
    ";

    /// <summary>
    /// Represents the template update query.
    /// </summary>
    public const string UpdateEmailTemplate = @"
        UPDATE [ns].[EmailTemplate]
        SET Subject = @Subject, Body = @Body, UpdatedBy = @UpdatedBy, UpdatedOn = @UpdatedOn 
        WHERE EmailTemplateId = @EmailTemplateId AND IsDeleted = 0;
    ";

    /// <summary>
    /// Represents delete template query.
    /// </summary>
    public const string DeleteEmailTemplate = @"
        UPDATE [ns].[EmailTemplate] 
        SET IsDeleted = 1, DeletedOn = @DeletedOn, DeletedBy = @DeletedBy  
        WHERE EmailTemplateId = @EmailTemplateId AND IsDeleted = 0
    ";
}