// -----------------------------------------------------------------------
// <copyright file="PushNotificationRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents the push notification repository.
/// </summary>
/// <seealso cref="BaseRepository" />
/// <seealso cref="Keka.Notifications.Core.Repositories.IPushNotificationRequestRepository" />
internal class PushNotificationRequestRepository : IPushNotificationRequestRepository
{
    private const string PushNotificationsTableName = "PushNotificationRequest";
    private readonly ITableStorageService tableStorageService;
    private readonly IMapper mapper;
    private readonly IAppContext appContext;
    private readonly IDateTimeService dateTimeService;

    public PushNotificationRequestRepository(ITableStorageService tableStorageService, IMapper mapper, IAppContext appContext, IDateTimeService dateTimeService)
    {
        this.tableStorageService = tableStorageService;
        this.mapper = mapper;
        this.appContext = appContext;
        this.dateTimeService = dateTimeService;
    }

    /// <inheritdoc/>
    public async Task<PushNotificationRequest> GetPushNotificationRequestAsync(Guid pushNotificationRequestId, string partitionKey)
    {
        var pushNotificationRequest = await this.tableStorageService.GetAsync<DbPushNotificationRequest>(PushNotificationsTableName, partitionKey, pushNotificationRequestId.ToString());
        return this.mapper.Map<PushNotificationRequest>(pushNotificationRequest);
    }

    /// <inheritdoc/>
    public async Task<(Guid requestId, string partitionKey)> InsertPushNotificationRequestAsync(PushNotificationRequest pushNotificationRequest)
    {
        var rowKey = Guid.NewGuid();
        var dbPushNotificationRequest = this.mapper.Map<DbPushNotificationRequest>(pushNotificationRequest);
        dbPushNotificationRequest.CreatedBy = this.appContext.UserId;
        dbPushNotificationRequest.CreatedOn = this.dateTimeService.GetCurrentTimeUtc();
        dbPushNotificationRequest.TenantId = this.appContext.TenantId;
        dbPushNotificationRequest.PartitionKey = this.GetPartitionKey();
        dbPushNotificationRequest.RowKey = rowKey.ToString();
        await this.tableStorageService.AddAsync<DbPushNotificationRequest>(PushNotificationsTableName, dbPushNotificationRequest);
        return (requestId: rowKey, partitionKey: dbPushNotificationRequest.PartitionKey);
    }

    /// <inheritdoc/>
    public async Task UpdatePushNotificationRequestStatusAsync(PushNotificationRequest pushNotificationRequest)
    {
        var dbPushNotificationRequest = this.mapper.Map<DbPushNotificationRequest>(pushNotificationRequest);
        dbPushNotificationRequest.TenantId = this.appContext.TenantId;
        dbPushNotificationRequest.PartitionKey = this.GetPartitionKey();
        dbPushNotificationRequest.RowKey = pushNotificationRequest.PushNotificationRequestId.ToString();
        dbPushNotificationRequest.UpdatedBy = this.appContext.UserId;
        dbPushNotificationRequest.UpdatedOn = this.dateTimeService.GetCurrentTimeUtc();
        await this.tableStorageService.UpdateAsync<DbPushNotificationRequest>(PushNotificationsTableName, dbPushNotificationRequest);
    }

    private string GetPartitionKey()
    {
        return $"{this.dateTimeService.GetCurrentYearMonth()}_{this.appContext.TenantId}";
    }
}
