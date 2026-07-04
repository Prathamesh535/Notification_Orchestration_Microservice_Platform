namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents the push notification template repository.
/// </summary>
/// <param name="mapper">The automapper instance.</param>
/// <param name="tableStorageService">The table storage service.</param>
internal class PushNotificationTemplateRepository(IMapper mapper, ITableStorageService tableStorageService)
    : IPushNotificationTemplateRepository
{
    private const string TableName = "PushNotificationTemplate";
    private const string PartitionKey = "00000000-0000-0000-0000-000000000000";

    /// <inheritdoc/>
    public async Task<PushNotificationTemplate> GetPushNotificationTemplateByIdAsync(Guid pushNotificationTemplateId)
    {
        var queryOptions = new QueryOptions<DbPushNotificationTemplate>()
        {
            Filter = x => x.PartitionKey.Equals(PartitionKey) && x.PushNotificationTemplateId == pushNotificationTemplateId,
        };
        var pushNotificationTemplate = (await tableStorageService.GetListAsync(TableName, queryOptions)).Items.SingleOrDefault();
        return mapper.Map<PushNotificationTemplate>(pushNotificationTemplate);
    }

    /// <inheritdoc/>
    public async Task<PushNotificationTemplate> GetPushNotificationTemplateByNameAsync(string pushNotificationTemplateName)
    {
        var pushNotificationTemplate = await tableStorageService.GetAsync<DbPushNotificationTemplate>(TableName, PartitionKey, pushNotificationTemplateName);
        return mapper.Map<PushNotificationTemplate>(pushNotificationTemplate);
    }
}
