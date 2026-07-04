namespace Keka.Notifications.Infrastructure.Database.DbModels;

[Table("ns.Employee")]
internal class DbEmployee : DbBaseModel
{
    [Key]
    public int EmployeeId { get; set; }
    public Guid Identifier { get; set; }
    public string DisplayName { get; set; }
}
