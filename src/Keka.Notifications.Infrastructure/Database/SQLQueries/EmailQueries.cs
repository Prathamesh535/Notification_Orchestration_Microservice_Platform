namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

internal static class EmailQueries
{
    public const string SelectEmailById = @"
        SELECT
            [EmailId],
            [EmailRequestId],
            [From],
            [Recipients],
            [ExternalId],
            [Content],
            [ReplyTo],
            [Attachments],
            [UpdatedOn]
        FROM
            [ns].[Email] WITH (NOLOCK)
        WHERE
            [EmailId] = @EmailId";

    public const string InsertEmail = @"
        INSERT INTO [ns].[Email]
            ([EmailId],
            [From],
            [Recipients],
            [Content],
            [ReplyTo],
            [Attachments],
            [EmailRequestId],
            [ExternalId],
            [Status],
            [TenantId],
            [CreatedOn],
            [CreatedBy]) OUTPUT INSERTED.EmailId
        VALUES
            (DEFAULT,
            @From,
            @Recipients,
            @Content,
            @ReplyTo,
            @Attachments,
            @EmailRequestId,
            @ExternalId,
            @Status,
            @TenantId,
            @CreatedOn,
            @CreatedBy)";

    public const string UpdateEmailStatus = @"
        UPDATE
            [ns] .[Email]
        SET
            [ExternalId] = @ExternalId,
            [Status] = @Status,
            [UpdatedOn] = @UpdatedOn,
            [UpdatedBy] = @UpdatedBy
        WHERE
            [EmailId] = @EmailId";

    public const string DeleteEmailById = @"
        DELETE FROM
          [ns].[Email]
        WHERE 
          [EmailId] = @EmailId";
}
