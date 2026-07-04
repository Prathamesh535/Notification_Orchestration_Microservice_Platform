// -----------------------------------------------------------------------
// <copyright file="AllowOnlyIfEnabledAttribute.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.WebAPI.Filters;

/// <summary>
/// Represents the filter that verifies if a specific type of notification is enabled based on configuration settings.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class AllowOnlyIfEnabledAttribute : Attribute, IAuthorizationFilter
{
    private readonly NotificationType notificationType;

    /// <summary>
    /// Initializes a new instance of the <see cref="AllowOnlyIfEnabledAttribute"/> class.
    /// </summary>
    /// <param name="notificationType">The notification type.</param>
    public AllowOnlyIfEnabledAttribute(NotificationType notificationType)
    {
        this.notificationType = notificationType;
    }

    /// <summary>
    /// Called after the action method executes.
    /// </summary>
    /// <param name="context">The action executed context.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

        // Form the key to check
        string key = $"{this.notificationType}:Enabled";

        // Check if the notification type is enabled
        bool isKeyPresent = configuration.GetSection(key).Exists();
        if (isKeyPresent && bool.TryParse(configuration[key], out var isEnabled) && !isEnabled)
        {
            context.Result = new ContentResult
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Content = $"{this.notificationType} sending is disabled.",
            };
        }
    }
}
