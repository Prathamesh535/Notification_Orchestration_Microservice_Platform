// -----------------------------------------------------------------------
// <copyright file="SlackNotificationTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// The slack notification template repository.
/// </summary>
/// <param name="mapper">The automapper instance.</param>
/// <param name="tableStorageService">The Table storage.</param>
internal class SlackNotificationTemplateRepository(IMapper mapper, ITableStorageService tableStorageService)
    : ISlackNotificationTemplateRepository
{
    private const string TableName = "SlackNotificationTemplate";
    private const string PartitionKey = "00000000-0000-0000-0000-000000000000";

    public async Task<SlackNotificationTemplate> GetSlackNotificationTemplateAsync(Guid templateId)
    {
        var queryOptions = new QueryOptions<DbSlackNotificationTemplate>()
        {
            Filter = x => x.PartitionKey.Equals(PartitionKey) && x.SlackNotificationTemplateId == templateId,
        };
        var template = (await tableStorageService.GetListAsync(TableName, queryOptions)).Items.SingleOrDefault();
        return mapper.Map<SlackNotificationTemplate>(template);
    }

    public async Task<SlackNotificationTemplate> GetSlackNotificationTemplateByNameAsync(string name)
    {
        var dbTemplate = await tableStorageService.GetAsync<DbSlackNotificationTemplate>(TableName, PartitionKey, name);
        return mapper.Map<SlackNotificationTemplate>(dbTemplate);
    }
}
