// -----------------------------------------------------------------------
// <copyright file="IWaTemplateRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents whatsapp template repository interface.
/// </summary>
public interface IWaTemplateRepository
{
    /// <summary>
    /// Get the template data using template id.
    /// </summary>
    /// <param name="waTemplateId">The template id.</param>
    /// <returns>Task the eventually return WaTemplate data.</returns>
    Task<WaTemplate> GetWaTemplateByIdAsync(Guid waTemplateId);

    /// <summary>
    /// Get the template data using template name.
    /// </summary>
    /// <param name="waTemplateName">The template name.</param>
    /// <returns>Task the eventually return WaTemplate data.</returns>
    Task<WaTemplate> GetWaTemplateByNameAsync(string waTemplateName);
}