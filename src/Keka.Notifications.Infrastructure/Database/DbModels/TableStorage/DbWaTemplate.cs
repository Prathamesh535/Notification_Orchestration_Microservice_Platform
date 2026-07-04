namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbWaTemplate : TableEntity
{
    public Guid WaTemplateId { get; set; }
    public string Name { get; set; }
    public string Language { get; set; }
    public string ExternalTemplateId { get; set; }
    public string ParameterMapping { get; set; }
}
