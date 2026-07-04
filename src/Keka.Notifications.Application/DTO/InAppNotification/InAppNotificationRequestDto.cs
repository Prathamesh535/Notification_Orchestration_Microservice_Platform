// -----------------------------------------------------------------------
// <copyright file="InAppNotificationRequestDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.InAppNotification;

/// <summary>
/// Represents the in app notification request dto.
/// </summary>
public class InAppNotificationRequestDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InAppNotificationRequestDto"/> class.
    /// </summary>
    public InAppNotificationRequestDto()
    {
        this.Personalization = new List<InAppNotificationPersonalizationDto>();
    }

    /// <summary>
    /// Gets or sets the template ID.
    /// </summary>
#nullable enable
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template Name.
    /// </summary>
    public string? TemplateName { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the list of personalization.
    /// </summary>
    public List<InAppNotificationPersonalizationDto> Personalization { get; set; }
}