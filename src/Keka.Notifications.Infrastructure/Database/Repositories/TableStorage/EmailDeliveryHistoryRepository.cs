// -----------------------------------------------------------------------
// <copyright file="EmailDeliveryHistoryRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents the employee repository.
/// </summary>
/// <param name="tableStorageService">The table storage service instance.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="appContext">The app cont.</param>
internal class EmailDeliveryHistoryRepository(ITableStorageService tableStorageService, IMapper mapper, IAppContext appContext)
    : IEmailDeliveryHistoryRepository
{
    private const string EmailsTableName = "EmailDeliveryHistory";
    private const string RawDataTableName = "EmailDeliveryRawData";

    /// <inheritdoc/>
    public async Task UpsertEmailHistoryAsync(List<EmailDeliveryHistory> emailDeliveryHistories)
    {
        // Convert to database models.
        var dbEmailDeliveryHistories = mapper.Map<IEnumerable<DbEmailDeliveryHistory>>(emailDeliveryHistories);

        // Remove duplicates based on partition key and row key.
        dbEmailDeliveryHistories = dbEmailDeliveryHistories.DistinctBy(e => (e.PartitionKey, e.RowKey));

        // Upsert records.
        await tableStorageService.InsertOrMergeBatchAsync(EmailsTableName, dbEmailDeliveryHistories);
    }

    /// <inheritdoc/>
    public async Task UpsertEmailDeliveryRawDataAsync(List<EmailDeliveryRawData> emailDeliveryRawDataRecords)
    {
        var dbEmailDeliveryRawDataRecords = mapper.Map<List<DbEmailDeliveryRawData>>(emailDeliveryRawDataRecords);
        await tableStorageService.InsertOrReplaceBatchAsync(RawDataTableName, dbEmailDeliveryRawDataRecords);
    }

    /// <inheritdoc/>
    public async Task<PagedResponse<EmailDeliveryHistory>> GetEmailDeliveryHistoryAsync(GetEmailHistoryRequest getEmailHistoryRequest)
    {
        var emailHistoryRecords = await tableStorageService.GetListAsync(EmailsTableName, new QueryOptions<DbEmailDeliveryHistory>(getEmailHistoryRequest.PageSize)
        {
            Filter = x => x.PartitionKey == getEmailHistoryRequest.Email && x.TenantId == appContext.TenantId && x.Timestamp >= getEmailHistoryRequest.FromDate && x.Timestamp <= getEmailHistoryRequest.ToDate,
            ContinuationToken = getEmailHistoryRequest.ContinuationToken,
        });

        var pagedEmailHistoryRecords = new PagedResponse<EmailDeliveryHistory>
        {
            Items = mapper.Map<List<EmailDeliveryHistory>>(emailHistoryRecords.Items.OrderByDescending(x => x.SentOn)),
            ContinuationToken = emailHistoryRecords.ContinuationToken,
        };

        return pagedEmailHistoryRecords;
    }

    /// <inheritdoc/>
    public async Task<List<EmailDeliveryRawData>> GetRawEmailDeliveryHistoryAsync(string messageId)
    {
        // Query to get all raw data records associated with the given messageId
        var emailDeliveryRawDataRecords = await tableStorageService.GetListAsync(RawDataTableName, new QueryOptions<DbEmailDeliveryRawData>()
        {
            Filter = x => x.PartitionKey == messageId,
        });
        return mapper.Map<List<EmailDeliveryRawData>>(emailDeliveryRawDataRecords.Items);
    }
}