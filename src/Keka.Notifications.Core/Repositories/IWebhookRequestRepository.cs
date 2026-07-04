// -----------------------------------------------------------------------
// <copyright file="IWebhookRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents webhook request repository interface.
/// </summary>
public interface IWebhookRequestRepository
{
    /// <summary>
    /// Saves the webhook request.
    /// </summary>
    /// <param name="webhookRequest">Represents Webhook request object.</param>
    /// <returns>Task that eventually returns request Id and partition key of the created record.</returns>
    Task<(Guid requestId, string partitionKey)> AddWebhookRequestAsync(WebhookRequest webhookRequest);

    /// <summary>
    /// Gets the webhook request.
    /// </summary>
    /// <param name="webhookRequestId">Represents the row key of the azure table storage.</param>
    /// <param name="partitionKey">Partition Key.</param>
    /// <returns>Returns the webhook request data from the table storage.</returns>
    Task<WebhookRequest> GetWebhookRequestAsync(Guid webhookRequestId, string partitionKey);

    /// <summary>
    /// This method updates the status of a webhook notification using the response data from an HTTP request.
    /// </summary>
    /// <param name="webhookRequest">The webhook request.</param>
    /// <param name="partitionKey">Partition Key.</param>
    /// <returns>
    /// Returns the webhook request data from the table storage.
    /// </returns>
    Task UpdateWebhookNotificationStatusAsync(WebhookRequest webhookRequest, string partitionKey);
}
