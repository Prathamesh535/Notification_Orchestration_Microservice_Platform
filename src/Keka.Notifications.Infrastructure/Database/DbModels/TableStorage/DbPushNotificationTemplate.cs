namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbPushNotificationTemplate : TableEntity
{
    public Guid PushNotificationTemplateId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string TemplateParameters { get; set; }
    public string DataParameters { get; set; }
}
