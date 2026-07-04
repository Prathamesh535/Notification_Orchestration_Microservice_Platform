namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbInAppNotification : TableEntity
{
    public Guid? TenantId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string MetaData { get; set; }
    public string Url { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadOn { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedOn { get; set; }
}