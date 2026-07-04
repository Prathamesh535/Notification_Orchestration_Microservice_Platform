namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbInAppNotificationRequest : TableEntity
{
    public int Status { get; set; }
#nullable enable
    public Guid? TenantId { get; set; }
    public Guid? InAppNotificationTemplateId { get; set; }
    public string? Personalization { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? FailureReason { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public Guid? UpdatedBy { get; set; }
#nullable restore
}
