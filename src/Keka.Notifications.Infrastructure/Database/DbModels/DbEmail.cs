namespace Keka.Notifications.Infrastructure.Database.DbModels;

[Table("ns.Email")]
internal class DbEmail : DbBaseModel
{
    public Guid EmailId { get; set; }
    public string From { get; set; }
    public string Recipients { get; set; }
    public string Content { get; set; }
    public string ReplyTo { get; set; }
    public string Attachments { get; set; }
    public Guid EmailRequestId { get; set; }
    public string ExternalId { get; set; }
    public short Status { get; set; }
}
