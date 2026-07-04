namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbEmailDeliveryRawData : TableEntity
{
    public string EventType { get; set; }
    public string RawResponse { get; set; }
}
