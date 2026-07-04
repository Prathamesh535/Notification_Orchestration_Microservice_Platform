// -----------------------------------------------------------------------
// <copyright file="IWebhookRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services.Interfaces;

/// <summary>
/// Represents Webhook request service interface.
/// </summary>
public interface IWebhookRequestService
{
    /// <summary>
    /// Saves webhook request.
    /// </summary>
    /// <param name="webhookRequestDto">Webhook request object.</param>
    /// <returns>Returns the webhook request Id.</returns>
    Task<Guid> AddWebhookRequestAsync(WebhookRequestDto webhookRequestDto);
}
