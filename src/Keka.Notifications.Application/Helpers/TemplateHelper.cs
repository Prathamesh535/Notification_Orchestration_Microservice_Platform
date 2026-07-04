// -----------------------------------------------------------------------
// <copyright file="TemplateHelper.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Helpers;

/// <summary>
/// Represents an template helper.
/// </summary>
public class TemplateHelper : ITemplateHelper
{
    /// <inheritdoc />
    public string ReplacePlaceholders(string templateText, Dictionary<string, string> templateData)
    {
        if (string.IsNullOrWhiteSpace(templateText) || templateData is null)
        {
            return templateText;
        }

        var compiledTemplate = Handlebars.Compile(templateText);
        if (compiledTemplate is not null)
        {
            return compiledTemplate(templateData);
        }

        return templateText;
    }

    /// <inheritdoc />
    public string ReplacePlaceholders(HandlebarsTemplate<object, object> compiledTemplate, Dictionary<string, string> templateData)
    {
        if (templateData is null || compiledTemplate is null)
        {
            return null;
        }

        return compiledTemplate(templateData);
    }

    /// <inheritdoc />
    public HandlebarsTemplate<object, object> CompileTemplate(string templateText)
    {
        if (string.IsNullOrWhiteSpace(templateText))
        {
            return null;
        }

        return Handlebars.Compile(templateText);
    }
}