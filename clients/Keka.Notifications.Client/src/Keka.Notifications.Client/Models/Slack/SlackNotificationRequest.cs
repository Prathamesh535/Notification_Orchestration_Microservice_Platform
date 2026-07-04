// -----------------------------------------------------------------------
// <copyright file="SlackNotificationRequestDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.Slack;

/// <summary>
/// Slack notification request dto.
/// </summary>
public class SlackNotificationRequest
{
#nullable enable
    /// <summary>
    /// Gets or sets slack notification template id.
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets slack notification template name.
    /// </summary>
    public string? TemplateName { get; set; }
#nullable restore
    /// <summary>
    /// Gets or sets slack url endpoint.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets slack Payload data.
    /// </summary>
    public object Payload { get; set; }
}
