// -----------------------------------------------------------------------
// <copyright file="DbSlackNotificationRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbSlackNotificationRequest : TableEntity
{
    public string TemplateId { get; set; }
    public string SlackUrl { get; set; }
    public string Payload { get; set; }
    public int NotificationStatus { get; set; }
    public bool HasException { get; set; }
    public Guid? TenantId { get; set; }
    public DateTime TriggerOn { get; set; }
    public string TriggerBy { get; set; }
#nullable enable
    public DateTime? SendOn { get; set; }
    public string? RawResponse { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public Guid? UpdatedBy { get; set; }
#nullable restore
}
