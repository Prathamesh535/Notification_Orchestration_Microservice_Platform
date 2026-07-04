// -----------------------------------------------------------------------
// <copyright file="WaPersonalization.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.Wa;

/// <summary>
/// Represents WhatsApp recipient list with associated template data.
/// </summary>
public class WaPersonalization
{
    /// <summary>
    /// Gets or sets list of recipients.
    /// </summary>
    public List<string> Recipients { get; set; }

    /// <summary>
    /// Gets or sets the template data associated with each recipient in the recipient list.
    /// </summary>
    public Dictionary<string, string> TemplateData { get; set; }

    /// <summary>
    /// Gets or sets attachments.
    /// </summary>
    public List<WaAttachment> Attachments { get; set; }
}
