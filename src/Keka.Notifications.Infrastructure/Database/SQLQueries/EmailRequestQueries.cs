namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

internal static class EmailRequestQueries
{
    public const string SelectEmailRequestById = @"
        SELECT
            [EmailRequestId],
            [BatchId],
            [Personalization],
            [From],
            [ReplyTo],
            [Content],
            [Attachments],
            [TemplateId],
            [SendAt],
            [TrackingSettings]
        FROM [ns].[EmailRequest] WITH (NOLOCK)
        WHERE [EmailRequestId] = @EmailRequestId";

    public const string InsertEmailRequest = @"
        INSERT INTO [ns].[EmailRequest]
        (
            [EmailRequestId],
            [BatchId],
            [Personalization],
            [From],
            [ReplyTo],
            [Content],
            [Attachments],
            [TemplateId],
            [SendAt],
            [TrackingSettings],
            [TenantId],
            [CreatedOn],
            [CreatedBy]
        ) OUTPUT INSERTED.EmailRequestId
        VALUES
        (
            DEFAULT,
            @BatchId,
            @Personalization,
            @From,
            @ReplyTo,
            @Content,
            @Attachments,
            @TemplateId,
            @SendAt,
            @TrackingSettings,
            @TenantId,
            @CreatedOn,
            @CreatedBy
        )";

    public const string DeleteEmailRequestById = @"
        DELETE FROM 
         [ns].[EmailRequest]
        WHERE
         EmailRequestId = @EmailRequestId";
}
