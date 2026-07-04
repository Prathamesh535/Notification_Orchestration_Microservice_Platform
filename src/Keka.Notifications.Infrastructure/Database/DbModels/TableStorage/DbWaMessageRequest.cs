// -----------------------------------------------------------------------
// <copyright file="DbWaMessageRequest.cs" company="Keka Inc">
// Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbWaMessageRequest : TableEntity
{
    public string Personalization { get; set; }
    public int Status { get; set; }
#nullable enable
    public Guid? WaTemplateId { get; set; }
    public string? ExternalId { get; set; }
    public string? FailureReason { get; set; }
    public Guid? TenantId { get; set; }
    public DateTime? CreatedOn { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public Guid? UpdatedBy { get; set; }
#nullable restore
}
