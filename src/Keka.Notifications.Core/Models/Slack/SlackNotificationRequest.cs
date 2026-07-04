// -----------------------------------------------------------------------
// <copyright file="SlackNotificationRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Slack;

/// <summary>
/// Slack notification request.
/// </summary>
public class SlackNotificationRequest
{
    /// <summary>
    /// Gets or sets slack request id.
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets slack url endpoint.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets payload.
    /// </summary>
    public object Payload { get; set; }

#nullable enable
    /// <summary>
    /// Gets or sets slack notification request id.
    /// </summary>
    public Guid? SlackNotificationRequestId { get; set; }

    /// <summary>
    /// Gets or sets NotificationStatus.
    /// </summary>
    public int? NotificationStatus { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether request has exception.
    /// </summary>
    public bool? HasException { get; set; }

    /// <summary>
    /// Gets or sets TenantId.
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Gets or sets triggered On.
    /// </summary>
    public DateTime? TriggerOn { get; set; }

    /// <summary>
    /// Gets or sets triggered by.
    /// </summary>
    public string? TriggerBy { get; set; }

    /// <summary>
    /// Gets or sets send on.
    /// </summary>
    public DateTime? SendOn { get; set; }

    /// <summary>
    /// Gets or sets Raw Response.
    /// </summary>
    public string? RawResponse { get; set; }

    /// <summary>
    /// Gets or sets Updated on.
    /// </summary>
    public DateTime? UpdatedOn { get; set; }

    /// <summary>
    /// Gets or sets Updated by.
    /// </summary>
    public Guid? UpdatedBy { get; set; }
#nullable restore
}
