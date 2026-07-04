// -----------------------------------------------------------------------
// <copyright file="ITemplateHelper.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Helpers.Interfaces;

/// <summary>
/// Represents interface of template helper.
/// </summary>
public interface ITemplateHelper
{
    /// <summary>
    /// Replace placeholders.
    /// </summary>
    /// <param name="templateText">Template Text.</param>
    /// <param name="templateData">Template Data.</param>
    /// <returns>The string.</returns>
    string ReplacePlaceholders(string templateText, Dictionary<string, string> templateData);

    /// <summary>
    /// Replace placeholders.
    /// </summary>
    /// <param name="compiledTemplate">Compiled template.</param>
    /// <param name="templateData">Template Data.</param>
    /// <returns>The string.</returns>
    string ReplacePlaceholders(HandlebarsTemplate<object, object> compiledTemplate, Dictionary<string, string> templateData);

    /// <summary>
    /// Compiles the template.
    /// </summary>
    /// <param name="templateText">Template Text.</param>
    /// <returns>The Handlerbar template.</returns>
    HandlebarsTemplate<object, object> CompileTemplate(string templateText);
}
