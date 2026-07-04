// -----------------------------------------------------------------------
// <copyright file="DbSmsRequest.cs" company="Keka Inc">
// Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

/// <summary>
/// Represents an Sms Request Containing SmsId,TemplateId,Personalization etc.
/// </summary>
internal class DbSmsRequest : TableEntity
{
    /// <summary>
    /// Gets or sets the Sms Template Id.
    /// </summary>
    public Guid? SmsTemplateId { get; set; }

#nullable enable
    /// <summary>
    /// Gets or sets the Personalization.
    /// </summary>
    public string? Personalization { get; set; }

    /// <summary>
    /// Gets or sets the External Id.
    /// </summary>
    public string? ExternalId { get; set; }

    /// <summary>
    /// Gets or sets the FailureReason.
    /// </summary>
    public string? FailureReason { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the tenant id.
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Gets or sets the created on.
    /// </summary>
    public DateTime? CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the created by.
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the updated on.
    /// </summary>
    public DateTime? UpdatedOn { get; set; }

    /// <summary>
    /// Gets or sets the updated by.
    /// </summary>
    public Guid? UpdatedBy { get; set; }
}
