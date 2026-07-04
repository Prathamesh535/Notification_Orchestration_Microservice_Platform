// -----------------------------------------------------------------------
// <copyright file="SmsRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

/// <summary>
/// Represents Sms Request Repository.
/// </summary>
/// <param name="tableStorageService">The table storage service.</param>
/// <param name="mapper">The automapper instance.</param>
/// <param name="appContext">The app context.</param>
/// <param name="dateTimeService">The date time service.</param>
internal class SmsRequestRepository(ITableStorageService tableStorageService, IMapper mapper, IAppContext appContext, IDateTimeService dateTimeService)
    : ISmsRequestRepository
{
    private const string SmsTableName = "SmsRequest";

    /// <inheritdoc/>
    public async Task<(Guid requestId, string partitionKey)> SaveSmsRequestAsync(SmsRequest smsRequest)
    {
        var dbSmsRequest = mapper.Map<DbSmsRequest>(smsRequest);
        this.SetCreateAuditFields(dbSmsRequest);

        var rowKey = Guid.NewGuid();
        dbSmsRequest.PartitionKey = this.GetPartitionKey();
        dbSmsRequest.RowKey = rowKey.ToString();

        await tableStorageService.AddAsync(SmsTableName, dbSmsRequest);
        return (requestId: rowKey, partitionKey: dbSmsRequest.PartitionKey);
    }

    /// <inheritdoc/>
    public async Task<SmsRequest> GetSmsRequestAsync(Guid smsRequestId, string partitionKey)
    {
        var smsRequest = await tableStorageService.GetAsync<DbSmsRequest>(SmsTableName, partitionKey, smsRequestId.ToString());
        return mapper.Map<SmsRequest>(smsRequest);
    }

    /// <inheritdoc/>
    public async Task UpdateSmsRequestStatusAsync(SmsRequest smsRequest, string partitionKey)
    {
        var dbSmsRequest = mapper.Map<DbSmsRequest>(smsRequest);
        this.SetUpdateAuditFields(dbSmsRequest);
        dbSmsRequest.PartitionKey = partitionKey;
        await tableStorageService.UpdateAsync(SmsTableName, dbSmsRequest);
    }

    private void SetCreateAuditFields(DbSmsRequest dbSmsRequest)
    {
        dbSmsRequest.TenantId = appContext.TenantId;
        dbSmsRequest.CreatedOn = dateTimeService.GetCurrentTimeUtc();
        dbSmsRequest.CreatedBy = appContext.UserId;
    }

    private void SetUpdateAuditFields(DbSmsRequest dbSmsRequest)
    {
        dbSmsRequest.UpdatedOn = dateTimeService.GetCurrentTimeUtc();
        dbSmsRequest.UpdatedBy = appContext.UserId;
    }

    private string GetPartitionKey()
    {
        var currentUtcYearMonth = dateTimeService.GetCurrentTimeUtc().ToString("yyyyMM");
        return $"{currentUtcYearMonth}_{appContext.TenantId}";
    }
}
