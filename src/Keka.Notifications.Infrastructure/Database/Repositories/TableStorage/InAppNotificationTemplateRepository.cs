// -----------------------------------------------------------------------
// <copyright file="InAppNotificationTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents the in app notification template repository.
/// </summary>
/// <param name="mapper">The automapper instance.</param>
/// <param name="tableStorageService">The table storage service instance.</param>
internal class InAppNotificationTemplateRepository(IMapper mapper, ITableStorageService tableStorageService)
    : IInAppNotificationTemplateRepository
{
    private const string TableName = "InAppNotificationTemplate";
    private const string PartitionKey = "00000000-0000-0000-0000-000000000000";

    /// <inheritdoc/>
    public async Task<InAppNotificationTemplate> GetInAppNotificationTemplateByIdAsync(Guid inAppNotificationTemplateId)
    {
        var queryOptions = new QueryOptions<DbInAppNotificationTemplate>()
        {
            Filter = x => x.PartitionKey.Equals(PartitionKey) && x.InAppNotificationTemplateId == inAppNotificationTemplateId,
        };
        var dbInAppNotificationTemplate = (await tableStorageService.GetListAsync(TableName, queryOptions)).Items.SingleOrDefault();
        return mapper.Map<InAppNotificationTemplate>(dbInAppNotificationTemplate);
    }

    /// <inheritdoc/>
    public async Task<InAppNotificationTemplate> GetInAppNotificationTemplateByNameAsync(string inAppNotificationTemplateName)
    {
        var dbInAppNotificationTemplate = await tableStorageService.GetAsync<DbInAppNotificationTemplate>(TableName, PartitionKey, inAppNotificationTemplateName);
        return mapper.Map<InAppNotificationTemplate>(dbInAppNotificationTemplate);
    }
}