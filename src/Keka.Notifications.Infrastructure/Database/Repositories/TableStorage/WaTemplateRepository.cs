// -----------------------------------------------------------------------
// <copyright file="WaTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents whatsapp template repository.
/// </summary>
/// <param name="db">The database context.</param>
/// <param name="mapper">The automapper instance.</param>
/// <param name="appContext">The app context.</param>
/// <param name="tableStorageService">The table storage service instance.</param>
internal class WaTemplateRepository(DatabaseContext db, IMapper mapper, IAppContext appContext, ITableStorageService tableStorageService)
    : BaseRepository(db, mapper, appContext), IWaTemplateRepository
{
    private const string TableName = "WaTemplate";
    private const string PartitionKey = "00000000-0000-0000-0000-000000000000";

    /// <inheritdoc/>
    public async Task<WaTemplate> GetWaTemplateByIdAsync(Guid waTemplateId)
    {
        var queryOptions = new QueryOptions<DbWaTemplate>()
        {
            Filter = x => x.PartitionKey.Equals(PartitionKey) && x.WaTemplateId == waTemplateId,
        };
        var dbWaTemplate = (await tableStorageService.GetListAsync(TableName, queryOptions)).Items.SingleOrDefault();
        return this.Mapper.Map<WaTemplate>(dbWaTemplate);
    }

    /// <inheritdoc/>
    public async Task<WaTemplate> GetWaTemplateByNameAsync(string waTemplateName)
    {
        var dbWaTemplate = await tableStorageService.GetAsync<DbWaTemplate>(TableName, PartitionKey, waTemplateName);
        return this.Mapper.Map<WaTemplate>(dbWaTemplate);
    }
}