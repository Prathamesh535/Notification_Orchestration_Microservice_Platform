namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbPushNotificationRequest : TableEntity
{
    public Guid PushNotificationTemplateId { get; set; }
    public string Personalization { get; set; }
    public int Status { get; set; }
    public Guid? TenantId { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }
#nullable enable
    public string? ExternalId { get; set; }
    public string? FailureReason { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public Guid? UpdatedBy { get; set; }
#nullable restore
}
