// -----------------------------------------------------------------------
// <copyright file="WebhookRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Service for handling webhook requests, including their creation, management, and processing.
/// </summary>
/// <param name="mapper">The mapper instance used for mapping between domain models and data transfer objects (DTOs).</param>
/// <param name="eventBus">The event bus instance used for publishing and subscribing to events related to webhook requests.</param>
/// <param name="appContext">The application context instance providing information about the current application environment and tenant.</param>
/// <param name="webhookRequestRepository">The webhook request repository.</param>
public partial class WebhookRequestService(IMapper mapper, IEventBus eventBus, IAppContext appContext, IWebhookRequestRepository webhookRequestRepository)
    : IWebhookRequestService
{
    private const string UrlPattern = "(https://).*";

    /// <summary>
    /// Adds webhook request to the table storage.
    /// </summary>
    /// <param name="webhookRequestDto">Represents webhook request object.</param>
    /// <returns>Returns webhook request id.</returns>
    public async Task<Guid> AddWebhookRequestAsync(WebhookRequestDto webhookRequestDto)
    {
        if (!ValidateWebhookRequest(webhookRequestDto, out ErrorCode? errorCode, out string errorMessage))
        {
            throw new Exceptions.ApplicationException((ErrorCode)errorCode, errorMessage);
        }

        var webhookRequest = mapper.Map<WebhookRequest>(webhookRequestDto);

        var (webhookRequestId, partitionKey) = await webhookRequestRepository.AddWebhookRequestAsync(webhookRequest);

        // Publish an event using EventBus to send a web hook notification
        await eventBus.PublishAsync(new SendWebhookNotificationEvent(appContext.TenantId, appContext.UserId, webhookRequestId, partitionKey));

        return webhookRequestId;
    }

    private static bool ValidateWebhookRequest(WebhookRequestDto webhookRequestDto, out ErrorCode? errorCode, out string errorMessage)
    {
        errorCode = null;
        errorMessage = null;

        // Validate HTTP Method
        if (!Enum.TryParse(webhookRequestDto.HttpMethod, ignoreCase: true, result: out HttpMethodType _))
        {
            errorCode = ErrorCode.INVALID_ARGS;
            errorMessage = "Invalid HTTP Method";
            return false;
        }

        if (!IsUrlValid(webhookRequestDto.EndPoint))
        {
            errorCode = ErrorCode.INVALID_ARGS;
            errorMessage = "Invalid Endpoint";
            return false;
        }

        return true;
    }

    [GeneratedRegex(UrlPattern)]
    private static partial Regex UrlRegex();

    private static bool IsUrlValid(string url)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                var regex = UrlRegex();
                return regex.IsMatch(url);
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}