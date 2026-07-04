// -----------------------------------------------------------------------
// <copyright file="EmailRequestDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Represents email request dto.
/// </summary>
public class EmailRequestDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRequestDto"/> class.
    /// </summary>
    public EmailRequestDto()
    {
        this.ReplyTo = new List<EmailAddressDto>();
        this.Personalization = new List<EmailPersonalizationDto>();
        this.Attachments = new List<EmailAttachmentDto>();
        this.TrackingSettings = new EmailTrackingSettingsDto();
    }

    /// <summary>
    /// Gets or sets the sender's email address.
    /// </summary>
    public EmailAddressDto From { get; set; }

    /// <summary>
    /// Gets or sets the reply-to email address.
    /// </summary>
    public List<EmailAddressDto> ReplyTo { get; set; }

    /// <summary>
    /// Gets or sets the list of personalization's for the email message.
    /// </summary>
    public List<EmailPersonalizationDto> Personalization { get; set; }

    /// <summary>
    /// Gets or sets the list of email attachments.
    /// </summary>
    public List<EmailAttachmentDto> Attachments { get; set; }

    /// <summary>
    /// Gets or sets the template ID for the email message.
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the list of email content.
    /// </summary>
    public EmailContentDto Content { get; set; }

    /// <summary>
    /// Gets or sets the timestamp to schedule the email message for sending.
    /// </summary>
    public DateTime? SendAt { get; set; }

    /// <summary>
    /// Gets or sets the tracking settings for the email message.
    /// </summary>
    public EmailTrackingSettingsDto TrackingSettings { get; set; }
}