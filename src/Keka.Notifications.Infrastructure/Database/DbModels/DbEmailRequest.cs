namespace Keka.Notifications.Infrastructure.Database.DbModels;

[Table("ns.Email")]
internal class DbEmailRequest : DbBaseModel
{
    [Key]
    public Guid EmailRequestId { get; set; }
    public string From { get; set; }
    public string ReplyTo { get; set; }
    public Guid? TemplateId { get; set; }
    public string Content { get; set; }
    public string Personalization { get; set; }
    public string Attachments { get; set; }
    public DateTime? SendAt { get; set; }
    public Guid? BatchId { get; set; }
    public string TrackingSettings { get; set; }
}