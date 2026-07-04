// -----------------------------------------------------------------------
// <copyright file="SmsTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents sms Template repository.
/// </summary>
/// <param name="mapper">The automapper instance.</param>
/// <param name="tableStorageService">The tablestorage service.</param>
internal class SmsTemplateRepository(IMapper mapper, ITableStorageService tableStorageService)
    : ISmsTemplateRepository
{
    private const string TableName = "SmsTemplate";
    private const string PartitionKey = "00000000-0000-0000-0000-000000000000";

    /// <inheritdoc/>
    public async Task<SmsTemplate> GetSmsTemplateByIdAsync(Guid smsTemplateId)
    {
        var queryOptions = new QueryOptions<DbSmsTemplate>()
        {
            Filter = x => x.PartitionKey.Equals(PartitionKey) && x.SmsTemplateId == smsTemplateId,
        };
        var dbSmsTemplate = (await tableStorageService.GetListAsync(TableName, queryOptions)).Items.SingleOrDefault();
        return mapper.Map<SmsTemplate>(dbSmsTemplate);
    }

    /// <inheritdoc/>
    public async Task<SmsTemplate> GetSmsTemplateByNameAsync(string smsTemplateName)
    {
        var dbSmsTemplate = await tableStorageService.GetAsync<DbSmsTemplate>(TableName, PartitionKey, smsTemplateName);
        return mapper.Map<SmsTemplate>(dbSmsTemplate);
    }
}
