// -----------------------------------------------------------------------
// <copyright file="IEmailTemplateService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents an email template service.
/// </summary>
public interface IEmailTemplateService
{
    /// <summary>
    /// Get email template by id.
    /// </summary>
    /// <param name="emailTemplateId">The email template id.</param>
    /// <returns>Return email template.</returns>
    Task<EmailTemplateDto> GetTemplateAsync(Guid emailTemplateId);

    /// <summary>
    /// Create a new email template.
    /// </summary>
    /// <param name="emailTemplateCreateDto">The email template create dto.</param>
    /// <returns>Return email template id.</returns>
    Task<Guid> CreateTemplateAsync(EmailTemplateCreateDto emailTemplateCreateDto);

    /// <summary>
    /// Update email template.
    /// </summary>
    /// <param name="emailTemplateId">The email template id.</param>
    /// <param name="emailTemplateDto">The email template dto.</param>
    /// <returns>Whether template updated.</returns>
    Task<bool> UpdateTemplateAsync(Guid emailTemplateId, EmailTemplateUpdateDto emailTemplateDto);

    /// <summary>
    /// Delete email template.
    /// </summary>
    /// <param name="emailTemplateId">The email template id.</param>
    /// <returns>Whether template deleted.</returns>
    Task<bool> DeleteTemplateAsync(Guid emailTemplateId);
}