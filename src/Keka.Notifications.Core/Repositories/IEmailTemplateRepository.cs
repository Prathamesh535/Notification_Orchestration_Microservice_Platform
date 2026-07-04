// -----------------------------------------------------------------------
// <copyright file="IEmailTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the email template repository.
/// </summary>
public interface IEmailTemplateRepository
{
    /// <summary>
    /// Get Email Template Async.
    /// </summary>
    /// <param name="emailTemplateId">The Email Template Id.</param>
    /// <returns>Email Template.</returns>
    Task<EmailTemplate> GetEmailTemplateAsync(Guid emailTemplateId);

    /// <summary>
    /// Save Email Template.
    /// </summary>
    /// <param name="emailTemplate">The Email Template.</param>
    /// <returns>Email Template Id.</returns>
    Task<Guid> SaveEmailTemplateAsync(EmailTemplate emailTemplate);

    /// <summary>
    /// Update Email Template.
    /// </summary>
    /// <param name="emailTemplate">The Email Template.</param>
    /// <returns>A <see cref="Task"/> returns task.</returns>
    Task UpdateEmailTemplateAsync(EmailTemplate emailTemplate);

    /// <summary>
    /// Delete Email Template.
    /// </summary>
    /// <param name="emailTemplate">The Email Template.</param>
    /// <returns>A <see cref="Task"/> returns task.</returns>
    Task DeleteEmailTemplateAsync(EmailTemplate emailTemplate);
}