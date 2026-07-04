namespace Keka.Notifications.Infrastructure.Database.DbModels;

[Table("[ns].[JobConfig]")]
internal class DbJobConfig : DbBaseModel
{
    [Key]
    public Guid JobConfigId { get; set; }

    public string JobType { get; set; }

    public string CronExpression { get; set; }

    public bool IsEnabled { get; set; }
}
