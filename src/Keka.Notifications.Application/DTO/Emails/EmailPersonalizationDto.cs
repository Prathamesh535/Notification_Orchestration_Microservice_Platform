// -----------------------------------------------------------------------
// <copyright file="EmailPersonalizationDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Represents the email personalization data transfer object.
/// </summary>
public class EmailPersonalizationDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailPersonalizationDto"/> class.
    /// </summary>
    public EmailPersonalizationDto()
    {
        this.DynamicTemplateData = new Dictionary<string, string>();
    }

    /// <summary>
    /// Gets or sets email personalization.
    /// </summary>
    public EmailRecipientsDto EmailRecipients { get; set; }

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the dynamic template data for the email.
    /// </summary>
    public Dictionary<string, string> DynamicTemplateData { get; set; }

    /// <summary>
    /// Gets or sets the timestamp at which the email should be sent.
    /// </summary>
    public long? SendAt { get; set; }
}
