namespace Keka.Notifications.Infrastructure.Database.Repositories.TableStorage;

internal class EmailTemplateRepository : IEmailTemplateRepository
{
    private const string EmailTemplateTableName = "EmailTemplate";
    private readonly IMapper mapper;
    private readonly IAppContext appContext;
    private readonly ITableStorageService tableStorageService;
    private readonly IDateTimeService dateTimeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplateRepository"/> class.
    /// </summary>
    /// <param name="mapper">The mapper instance.</param>
    /// <param name="appContext">The app context instance.</param>
    /// <param name="tableStorageService">The table storage service instance.</param>
    /// <param name="dateTimeService">The date time service instance.</param>
    public EmailTemplateRepository(IMapper mapper, IAppContext appContext, ITableStorageService tableStorageService, IDateTimeService dateTimeService)
    {
        this.mapper = mapper;
        this.appContext = appContext;
        this.tableStorageService = tableStorageService;
        this.dateTimeService = dateTimeService;
    }

    /// <inheritdoc/>
    public async Task<EmailTemplate> GetEmailTemplateAsync(Guid emailTemplateId)
    {
        var dbEmailTemplate = await this.tableStorageService.GetAsync<DbEmailTemplate>(EmailTemplateTableName, this.appContext.TenantId.ToString(), emailTemplateId.ToString());
        var emailTemplate = this.mapper.Map<EmailTemplate>(dbEmailTemplate);
        emailTemplate.EmailTemplateId = Guid.Parse(dbEmailTemplate.RowKey);
        return emailTemplate;
    }

    /// <inheritdoc/>
    public async Task<Guid> SaveEmailTemplateAsync(EmailTemplate emailTemplate)
    {
        var rowKey = Guid.NewGuid();
        var dbEmailTemplate = this.mapper.Map<DbEmailTemplate>(emailTemplate);
        dbEmailTemplate.RowKey = rowKey.ToString();
        dbEmailTemplate.PartitionKey = this.appContext.TenantId.ToString();
        dbEmailTemplate.CreatedBy = this.appContext.UserId;
        dbEmailTemplate.CreatedOn = this.dateTimeService.GetCurrentTimeUtc();
        dbEmailTemplate.IsDeleted = false;
        dbEmailTemplate.IsEnabled = true;
        await this.tableStorageService.AddAsync(EmailTemplateTableName, dbEmailTemplate);
        return rowKey;
    }

    /// <inheritdoc/>
    public async Task UpdateEmailTemplateAsync(EmailTemplate emailTemplate)
    {
        var dbEmailTemplate = this.mapper.Map<DbEmailTemplate>(emailTemplate);
        dbEmailTemplate.RowKey = emailTemplate.EmailTemplateId.ToString();
        dbEmailTemplate.PartitionKey = this.appContext.TenantId.ToString();
        dbEmailTemplate.UpdatedBy = this.appContext.UserId;
        dbEmailTemplate.UpdatedOn = this.dateTimeService.GetCurrentTimeUtc();
        await this.tableStorageService.UpdateAsync(EmailTemplateTableName, dbEmailTemplate);
    }

    /// <inheritdoc/>
    public async Task DeleteEmailTemplateAsync(EmailTemplate emailTemplate)
    {
        var dbEmailTemplate = this.mapper.Map<DbEmailTemplate>(emailTemplate);
        dbEmailTemplate.RowKey = emailTemplate.EmailTemplateId.ToString();
        dbEmailTemplate.PartitionKey = this.appContext.TenantId.ToString();
        dbEmailTemplate.IsDeleted = true;
        dbEmailTemplate.DeletedBy = this.appContext.UserId;
        dbEmailTemplate.DeletedOn = this.dateTimeService.GetCurrentTimeUtc();
        await this.tableStorageService.UpdateAsync(EmailTemplateTableName, dbEmailTemplate);
    }
}