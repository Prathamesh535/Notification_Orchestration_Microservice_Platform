// -----------------------------------------------------------------------
// <copyright file="WebhookNotificationSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Service responsible for sending webhook notifications.
/// Initializes a new instance of the <see cref="WebhookNotificationSenderService"/> class.
/// </summary>
/// <param name="logger">The logger instance.</param>
/// <param name="httpClient">The HTTP client used to send requests.</param>
/// <param name="webhookRequestRepository">The WebhookRequestRepository instance.</param>
public class WebhookNotificationSenderService(ILogger<WebhookNotificationSenderService> logger, IHttpClient httpClient, IWebhookRequestRepository webhookRequestRepository)
    : IWebhookNotificationSenderService
{
    /// <summary>
    /// Sends a webhook notification based on the provided event data.
    /// </summary>
    /// <param name="sendWebhookNotificationEvent">The event containing webhook request details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SendWebhookNotificationAsync(SendWebhookNotificationEvent sendWebhookNotificationEvent)
    {
        try
        {
            // Retrieve the webhook request based on the ID
            var webhookRequest = await webhookRequestRepository.GetWebhookRequestAsync(sendWebhookNotificationEvent.WebhookRequestId, sendWebhookNotificationEvent.PartitionKey);
            if (webhookRequest is null)
            {
                logger.LogInformation("Webhook request not found for WebhookRequestId : {WebhookRequestId}", sendWebhookNotificationEvent.WebhookRequestId);
                return;
            }

            // Create and send the HTTP request.
            HttpRequestMessage requestMessage = CreateHttpRequestMessage(webhookRequest);
            var httpResponse = await httpClient.SendAsync(requestMessage);

            // Update webhook notification status based on the response
            webhookRequest.RawResponse = await httpResponse.Content.ReadAsStringAsync();
            webhookRequest.NotificationStatus = NotificationStatus.Sent;
            webhookRequest.Payload = string.Empty;
            webhookRequest.RequestHeaders = new Dictionary<string, string>();
            await this.UpdateWebhookNotificationStatusAsync(webhookRequest, sendWebhookNotificationEvent.PartitionKey);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{errorMessage}", ex.Message);

            var failedWebhookRequest = new WebhookRequest
            {
                WebhookRequestId = sendWebhookNotificationEvent.WebhookRequestId,
                RawResponse = ex.Message,
                NotificationStatus = NotificationStatus.Failed,
            };
            await this.UpdateWebhookNotificationStatusAsync(failedWebhookRequest, sendWebhookNotificationEvent.PartitionKey);
        }
    }

    private static HttpRequestMessage CreateHttpRequestMessage(WebhookRequest webhookRequest)
    {
        // Prepare the request message
        var requestMessage = new HttpRequestMessage
        {
            Method = ConvertToHttpMethod(webhookRequest.HttpMethod),
            RequestUri = new UriBuilder(webhookRequest.EndPoint).Uri,
            Content = webhookRequest.Payload is not null ? new StringContent(webhookRequest.Payload.ToJson(), System.Text.Encoding.UTF8, "application/json") : null,
        };

        // Add request headers
        foreach (var header in webhookRequest.RequestHeaders ?? Enumerable.Empty<KeyValuePair<string, string>>())
        {
            if (header.Key is not null && header.Value is not null)
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        return requestMessage;
    }

    private static HttpMethod ConvertToHttpMethod(HttpMethodType httpMethodType)
    {
        return httpMethodType switch
        {
            HttpMethodType.GET => HttpMethod.Get,
            HttpMethodType.POST => HttpMethod.Post,
            HttpMethodType.PUT => HttpMethod.Put,
            HttpMethodType.DELETE => HttpMethod.Delete,
            HttpMethodType.PATCH => HttpMethod.Patch,
            _ => throw new NotSupportedException($"HTTP method {httpMethodType} is not supported")
        };
    }

    private async Task UpdateWebhookNotificationStatusAsync(WebhookRequest webhookRequest, string partitionKey)
    {
        await webhookRequestRepository.UpdateWebhookNotificationStatusAsync(webhookRequest, partitionKey);
    }
}
