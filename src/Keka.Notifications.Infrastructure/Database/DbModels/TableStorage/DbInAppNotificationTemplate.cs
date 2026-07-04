namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbInAppNotificationTemplate : TableEntity
{
    public Guid InAppNotificationTemplateId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Parameters { get; set; }
}