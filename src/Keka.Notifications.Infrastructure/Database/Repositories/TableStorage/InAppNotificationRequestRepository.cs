// -----------------------------------------------------------------------
// <copyright file="InAppNotificationRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents the in app notification request repository.
/// </summary>
/// <param name="mapper">The automapper instance.</param>
/// <param name="appContext">The app context.</param>
/// <param name="tableStorageService">Table Storage Service Instance.</param>
/// <param name="dateTimeService">The Date Time Service Instance.</param>
internal class InAppNotificationRequestRepository(IMapper mapper, IAppContext appContext, IDateTimeService dateTimeService, ITableStorageService tableStorageService)
    : IInAppNotificationRequestRepository
{
    private const string TableName = "InAppNotificationRequest";

    /// <inheritdoc/>
    public async Task<(string requestId, string partitionKey)> SaveInAppNotificationRequestAsync(InAppNotificationRequest inAppNotificationRequest)
    {
        var dbNotificationRequest = mapper.Map<DbInAppNotificationRequest>(inAppNotificationRequest);
        dbNotificationRequest.PartitionKey = this.GetPartitionKey();
        dbNotificationRequest.TenantId = appContext.TenantId;
        dbNotificationRequest.RowKey = dateTimeService.GetInvertedTicks();
        dbNotificationRequest.CreatedBy = appContext.UserId;
        dbNotificationRequest.CreatedOn = dateTimeService.GetCurrentTimeUtc();
        await tableStorageService.InsertOrMergeAsync(TableName, dbNotificationRequest);
        return (requestId: dbNotificationRequest.RowKey, partitionKey: dbNotificationRequest.PartitionKey);
    }

    /// <inheritdoc/>
    public async Task<InAppNotificationRequest> GetInAppNotificationRequestAsync(string inAppNotificationRequestId, string partitionKey)
    {
        var inAppNotificationRequest = await tableStorageService.GetAsync<DbInAppNotificationRequest>(TableName, partitionKey, inAppNotificationRequestId.ToString());
        return mapper.Map<InAppNotificationRequest>(inAppNotificationRequest);
    }

    /// <inheritdoc/>
    public async Task UpdateInAppNotificationRequestStatusAsync(InAppNotificationRequest inAppNotificationRequest, string partitionKey)
    {
        var dbNotificationRequest = mapper.Map<DbInAppNotificationRequest>(inAppNotificationRequest);
        dbNotificationRequest.InAppNotificationTemplateId = null;
        dbNotificationRequest.UpdatedBy = appContext.UserId;
        dbNotificationRequest.UpdatedOn = dateTimeService.GetCurrentTimeUtc();
        dbNotificationRequest.PartitionKey = partitionKey;
        await tableStorageService.UpdateAsync(TableName, dbNotificationRequest);
    }

    private string GetPartitionKey()
    {
        return $"{dateTimeService.GetCurrentYearMonth()}_{appContext.TenantId}";
    }
}
