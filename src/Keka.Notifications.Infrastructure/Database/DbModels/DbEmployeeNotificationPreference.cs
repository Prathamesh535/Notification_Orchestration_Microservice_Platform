namespace Keka.Notifications.Infrastructure.Database.DbModels;

[Table("ns.EmployeeNotificationPreference")]
internal class DbEmployeeNotificationPreference : DbBaseModel
{
    public Guid EmployeeId { get; set; }
    public Guid EventId { get; set; }
    public bool IsEnabled { get; set; }
}