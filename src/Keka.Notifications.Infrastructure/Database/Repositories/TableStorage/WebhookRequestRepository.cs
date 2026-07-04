// -----------------------------------------------------------------------
// <copyright file="WebhookRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents webhook request repository.
/// </summary>
/// <param name="tableStorageService">The table storage service.</param>
/// <param name="mapper">The automapper instance.</param>
/// <param name="appContext">The app context.</param>
/// <param name="dateTimeService">The date time service.</param>
internal class WebhookRequestRepository(ITableStorageService tableStorageService, IMapper mapper, IAppContext appContext, IDateTimeService dateTimeService)
    : IWebhookRequestRepository
{
    private const string WebhookTableName = "Webhook";

    public async Task<(Guid requestId, string partitionKey)> AddWebhookRequestAsync(WebhookRequest webhookRequest)
    {
        var dbWebhookRequest = this.MapDbWebhookRequest(webhookRequest);
        await tableStorageService.AddAsync(WebhookTableName, dbWebhookRequest);
        return (Guid.Parse(dbWebhookRequest.RowKey), dbWebhookRequest.PartitionKey);
    }

    public async Task<WebhookRequest> GetWebhookRequestAsync(Guid webhookRequestId, string partitionKey)
    {
        var dbWebhookRequest = await tableStorageService.GetAsync<DbWebhookRequest>(WebhookTableName, partitionKey, webhookRequestId.ToString());
        return mapper.Map<WebhookRequest>(dbWebhookRequest);
    }

    public async Task UpdateWebhookNotificationStatusAsync(WebhookRequest webhookRequest, string partitionKey)
    {
        // Map WebhookRequest to DbWebhookRequest
        var dbWebhookRequest = mapper.Map<DbWebhookRequest>(webhookRequest);

        dbWebhookRequest.PartitionKey = partitionKey;
        dbWebhookRequest.SendOn = dateTimeService.GetCurrentTimeUtc();

        // Save the updated record back to table storage
        await tableStorageService.UpdateAsync(WebhookTableName, dbWebhookRequest);
    }

    private DbWebhookRequest MapDbWebhookRequest(WebhookRequest webhookRequest)
    {
        var dbWebhookRequest = mapper.Map<DbWebhookRequest>(webhookRequest);
        dbWebhookRequest.PartitionKey = this.GetPartitionKey();
        dbWebhookRequest.RowKey = Guid.NewGuid().ToString();
        dbWebhookRequest.TenantId = appContext.TenantId;
        dbWebhookRequest.TriggerOn = dateTimeService.GetCurrentTimeUtc();
        dbWebhookRequest.TriggerBy = appContext.UserId.ToString();
        return dbWebhookRequest;
    }

    private string GetPartitionKey()
    {
        var currentUtcYearMonth = dateTimeService.GetCurrentTimeUtc().ToString("yyyyMM");
        return $"{currentUtcYearMonth}_{appContext.TenantId}";
    }
}
