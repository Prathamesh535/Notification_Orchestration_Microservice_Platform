// -----------------------------------------------------------------------
// <copyright file="DbWebhookRequest.cs" company="Keka Inc">
// Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbWebhookRequest : TableEntity
{
    public DateTime? SendOn { get; set; }
    public string EndPoint { get; set; }
    public int NotificationStatus { get; set; }
    public bool HasException { get; set; }
    public string RawResponse { get; set; }
    public string TriggerBy { get; set; }
    public DateTime? TriggerOn { get; set; }
    public Guid? TenantId { get; set; }
    public string RequestHeaders { get; set; }
    public string Payload { get; set; }
    public int HttpMethod { get; set; }
}
