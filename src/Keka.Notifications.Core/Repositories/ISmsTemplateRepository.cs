// -----------------------------------------------------------------------
// <copyright file="ISmsTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents interface for Sms Template repository.
/// </summary>
public interface ISmsTemplateRepository
{
    /// <summary>
    /// Gets the sms template data using sms template Id.
    /// </summary>
    /// <param name="smsTemplateId">The sms template Id.</param>
    /// <returns>Task the returns sms template data.</returns>
    Task<SmsTemplate> GetSmsTemplateByIdAsync(Guid smsTemplateId);

    /// <summary>
    /// Gets the sms template data using sms template name.
    /// </summary>
    /// <param name="smsTemplateName">The sms template Name.</param>
    /// <returns>Task the returns sms template data.</returns>
    Task<SmsTemplate> GetSmsTemplateByNameAsync(string smsTemplateName);
}
