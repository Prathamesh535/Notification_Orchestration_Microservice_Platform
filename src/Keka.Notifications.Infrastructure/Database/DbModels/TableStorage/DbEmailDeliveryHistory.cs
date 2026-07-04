namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbEmailDeliveryHistory : TableEntity
{
    public string FromEmail { get; set; }
    public string Subject { get; set; }

#nullable enable
    public DateTime? SentOn { get; set; }
    public Guid? UserId { get; set; }
    public int? DeliveryStatusId { get; set; }
    public Guid? TenantId { get; set; }
    public int? FailedReason { get; set; }
    public bool? HasAttachment { get; set; }
#nullable restore
}
