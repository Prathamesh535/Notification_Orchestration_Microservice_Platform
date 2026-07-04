// -----------------------------------------------------------------------
// <copyright file="SlackNotificationTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Slack;

/// <summary>
/// Slack message request.
/// </summary>
public class SlackNotificationTemplate
{
    /// <summary>
    /// Gets or sets slack template id.
    /// </summary>
    public Guid SlackTemplateId { get; set; }

    /// <summary>
    /// Gets or sets name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets body.
    /// </summary>
    public string Body { get; set; }
}
