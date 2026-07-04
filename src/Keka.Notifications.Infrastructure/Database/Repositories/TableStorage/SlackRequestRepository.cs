// -----------------------------------------------------------------------
// <copyright file="SlackRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// The slack notification request repository.
/// </summary>
/// <param name="tableStorageService">The table storage service.</param>
/// <param name="mapper">The automapper instance.</param>
/// <param name="appContext">The app context.</param>
/// <param name="dateTimeService">The date time service.</param>
internal class SlackRequestRepository(ITableStorageService tableStorageService, IMapper mapper, IAppContext appContext, IDateTimeService dateTimeService)
    : ISlackRequestRepository
{
    private const string SlackNotificationRequestTableName = "Slack";

    /// <inheritdoc/>
    public async Task<(Guid requestId, string partitionKey)> SaveSlackNotificationRequestAsync(SlackNotificationRequest slackRequest)
    {
        slackRequest.TenantId = appContext.TenantId;
        slackRequest.NotificationStatus = (int)NotificationStatus.Queued;
        slackRequest.TriggerOn = dateTimeService.GetCurrentTimeUtc();
        slackRequest.TriggerBy = appContext.UserId.ToString();
        var dbSlackNotificationRequest = mapper.Map<DbSlackNotificationRequest>(slackRequest);
        var slackNotificationRequestId = Guid.NewGuid();
        dbSlackNotificationRequest.RowKey = slackNotificationRequestId.ToString();
        dbSlackNotificationRequest.PartitionKey = this.GetPartitionKey();
        await tableStorageService.InsertOrMergeAsync(SlackNotificationRequestTableName, dbSlackNotificationRequest);
        return (slackNotificationRequestId, dbSlackNotificationRequest.PartitionKey);
    }

    /// <inheritdoc/>
    public async Task<SlackNotificationRequest> GetSlackNotificationRequestAsync(Guid slackRequestId, string partitionKey)
    {
        var slackRequest = await tableStorageService.GetAsync<DbSlackNotificationRequest>(SlackNotificationRequestTableName, partitionKey, slackRequestId.ToString());
        return mapper.Map<SlackNotificationRequest>(slackRequest);
    }

    /// <inheritdoc/>
    public async Task UpdateSlackNotificationAsync(SlackNotificationRequest slackNotificationRequest, string partitionKey)
    {
        slackNotificationRequest.SendOn = slackNotificationRequest.UpdatedOn = dateTimeService.GetCurrentTimeUtc();
        slackNotificationRequest.UpdatedBy = appContext.UserId;
        var entity = mapper.Map<DbSlackNotificationRequest>(slackNotificationRequest);
        entity.PartitionKey = partitionKey;
        await tableStorageService.UpdateAsync(SlackNotificationRequestTableName, entity);
    }

    private string GetPartitionKey()
    {
        var currentUtcYearMonth = dateTimeService.GetCurrentTimeUtc().ToString("yyyyMM");
        return $"{currentUtcYearMonth}_{appContext.TenantId}";
    }
}
