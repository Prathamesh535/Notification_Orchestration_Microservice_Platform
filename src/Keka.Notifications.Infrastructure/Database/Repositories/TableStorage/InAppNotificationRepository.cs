// -----------------------------------------------------------------------
// <copyright file="InAppNotificationRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents the in app notification repository.
/// </summary>
/// <param name="mapper">The automapper instance.</param>
/// <param name="appContext">The app context.</param>
/// <param name="tableStorageService">The table storage service.</param>
/// <param name="dateTimeService">The date time service.</param>
/// <seealso cref="IInAppNotificationRepository" />
internal class InAppNotificationRepository(IMapper mapper, IAppContext appContext, ITableStorageService tableStorageService, IDateTimeService dateTimeService)
    : IInAppNotificationRepository
{
    private const string TableName = "InAppNotification";

    /// <inheritdoc/>
    public async Task<InAppNotification> GetInAppNotification(Guid employeeId, string inAppNotificationId)
    {
        var inAppNotification = await tableStorageService.GetAsync<DbInAppNotification>(TableName, employeeId.ToString(), inAppNotificationId);
        return mapper.Map<InAppNotification>(inAppNotification);
    }

    /// <inheritdoc/>
    public async Task<PagedResponse<InAppNotification>> GetInAppNotifications(Guid employeeId, GetInAppNotificationRequest getInAppNotificationRequest)
    {
        var inAppNotificationRequests = await tableStorageService.GetListAsync(TableName, new QueryOptions<DbInAppNotification>(getInAppNotificationRequest.PageSize)
        {
            Filter = x => x.PartitionKey == employeeId.ToString(),
            ContinuationToken = getInAppNotificationRequest.ContinuationToken,
        });
        var pagedInAppNotificationRecords = new PagedResponse<InAppNotification>
        {
            Items = mapper.Map<List<InAppNotification>>(inAppNotificationRequests.Items),
            ContinuationToken = inAppNotificationRequests.ContinuationToken,
        };
        return pagedInAppNotificationRecords;
    }

    /// <inheritdoc/>
    public async Task<List<(Guid, string)>> AddInAppNotifications(List<InAppNotification> inAppNotifications)
    {
        var dbInAppNotifications = this.MapDbInAppNotifications(inAppNotifications);
        await tableStorageService.AddBatchAsync(TableName, dbInAppNotifications);
        return dbInAppNotifications.Select(notification => (Guid.Parse(notification.PartitionKey), notification.RowKey)).ToList();
    }

    /// <inheritdoc/>
    public async Task<bool> MarkNotificationAsRead(Guid employeeId, string inAppNotificationId)
    {
        var queryOptions = new QueryOptions<DbInAppNotification>()
        {
            Filter = x => x.PartitionKey == employeeId.ToString() && x.RowKey == inAppNotificationId && !x.IsRead,
        };
        var unReadNotification = (await tableStorageService.GetListAsync(TableName, queryOptions)).Items.SingleOrDefault();
        if (unReadNotification is not null)
        {
            unReadNotification.IsRead = true;
            unReadNotification.ReadOn = dateTimeService.GetCurrentTimeUtc();
            await tableStorageService.UpdateAsync(TableName, unReadNotification);
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> MarkAllNotificationsAsRead(Guid employeeId)
    {
        var notifications = await this.GetAllUnReadNotifications(employeeId);
        if (notifications is not null && notifications.Count > 0)
        {
            var currentUtcTime = dateTimeService.GetCurrentTimeUtc();
            notifications.ForEach(item =>
            {
                item.IsRead = true;
                item.ReadOn = currentUtcTime;
            });
            await tableStorageService.UpdateBatchAsync(TableName, notifications);
            return true;
        }

        return false;
    }

    private async Task<List<DbInAppNotification>> GetAllUnReadNotifications(Guid employeeId)
    {
        var queryOptions = new QueryOptions<DbInAppNotification>()
        {
            Filter = x => x.PartitionKey == employeeId.ToString() && !x.IsRead,
        };
        var unReadNotifications = await tableStorageService.GetListAsync(TableName, queryOptions);
        return unReadNotifications.Items.ToList();
    }

    private List<DbInAppNotification> MapDbInAppNotifications(List<InAppNotification> inAppNotifications)
    {
        var dbInAppNotifications = mapper.Map<List<DbInAppNotification>>(inAppNotifications);
        var currenTime = dateTimeService.GetCurrentTimeUtc();
        var rowKey = dateTimeService.GetInvertedTicks();
        dbInAppNotifications.ForEach(entity =>
        {
            if (string.IsNullOrWhiteSpace(entity.RowKey))
            {
                entity.RowKey = rowKey;
            }

            entity.TenantId = appContext.TenantId;
            entity.CreatedOn = currenTime;
        });

        return dbInAppNotifications;
    }
}