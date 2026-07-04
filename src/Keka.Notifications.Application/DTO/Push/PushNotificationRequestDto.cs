// -----------------------------------------------------------------------
// <copyright file="PushNotificationRequestDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Push;

/// <summary>
/// Represents the push notification request dto.
/// </summary>
public class PushNotificationRequestDto
{
    /// <summary>
    /// Gets or sets the template id.
    /// </summary>
#nullable enable
    public Guid? PushNotificationTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name.
    /// </summary>
    public string? PushNotificationTemplateName { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the personalization data.
    /// </summary>
    public List<PushNotificationPersonalizationDto> Personalization { get; set; }
}
