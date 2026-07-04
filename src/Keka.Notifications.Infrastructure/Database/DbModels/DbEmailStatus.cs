namespace Keka.Notifications.Infrastructure.Database.DbModels;

[Table("ns.EmailStatus")]
internal class DbEmailStatus : DbBaseModel
{
    [Key]
    public Guid EmailStatusId { get; set; }
#nullable enable
    public Guid? EmployeeId { get; set; }
#nullable restore
    public string Email { get; set; }
    public int NoOfEmailsSent { get; set; }
    public int NoOfEmailsFailed { get; set; }
#nullable enable
    public DateTime? LastEmailSentOn { get; set; }
    public DateTime? LastEmailDeliveredOn { get; set; }
    public DateTime? LastEmailFailedOn { get; set; }
    public short? LastEmailFailedReason { get; set; }
    public short? ConsecutiveBlockCount { get; set; }
#nullable restore
    public bool IsBlocked { get; set; }
#nullable enable
    public DateTime? LastBlockedOn { get; set; }
    public DateTime? BlockedUntil { get; set; }
    public string? PreviousStateDetails { get; set; }
#nullable restore
}