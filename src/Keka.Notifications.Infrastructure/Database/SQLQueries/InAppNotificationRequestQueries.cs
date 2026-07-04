// -----------------------------------------------------------------------
// <copyright file="InAppNotificationRequestQueries.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.SQLQueries;

/// <summary>
/// Represents the in app notification request queries.
/// </summary>
internal static class InAppNotificationRequestQueries
{
    /// <summary>
    /// The select in application notification request by identifier.
    /// </summary>
    public const string SelectInAppNotificationRequestById = @"
        SELECT 
            [InAppNotificationRequestId],
            [TenantId],
            [Personalization],
            [InAppNotificationTemplateId],
            [Status],
            [CreatedOn],
            [CreatedBy]
        FROM [ns].[InAppNotificationRequest]
        WHERE [InAppNotificationRequestId] = @InAppNotificationRequestId";

    /// <summary>
    /// The insert in application notification request.
    /// </summary>
    public const string InsertInAppNotificationRequest = @"
        INSERT INTO [ns].[InAppNotificationRequest]
        (
            [InAppNotificationRequestId],
            [TenantId],
            [Personalization],
            [InAppNotificationTemplateId],
            [Status],
            [CreatedOn],
            [CreatedBy]
        ) OUTPUT INSERTED.InAppNotificationRequestId
        VALUES
        (
            DEFAULT,
            @TenantId,
            @Personalization,
            @InAppNotificationTemplateId,
            @Status,
            @CreatedOn,
            @CreatedBy
        )";

    /// <summary>
    /// The insert in application notification request.
    /// </summary>
    public const string UpdateInAppNotificationRequestStatus = @"
        UPDATE 
            [ns].[InAppNotificationRequest]
        SET 
            [Status] = @Status, 
            [FailureReason] = @FailureReason, 
            [UpdatedBy] = @UpdatedBy, 
            [UpdatedOn] = @UpdatedOn 
        WHERE 
            [InAppNotificationRequestId] = @InAppNotificationRequestId;
    ";
}
