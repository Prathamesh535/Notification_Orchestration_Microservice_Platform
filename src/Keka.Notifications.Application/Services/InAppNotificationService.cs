// -----------------------------------------------------------------------
// <copyright file="InAppNotificationService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents an in app notification service.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="appContext">The app context instance.</param>
/// <param name="eventBus">The event bus.</param>
/// <param name="templateHelper">The template helper.</param>
/// <param name="inAppNotificationRequestRepository">The InAppNotificationRequestRepository instance.</param>
/// <param name="inAppNotificationTemplateRepository">The InAppNotificationTemplateRepository instance.</param>
/// <param name="inAppNotificationRepository">The InAppNotificationRepository instance.</param>
/// <param name="dateTimeService">The date time service instance.</param>
public class InAppNotificationService(ILogger<InAppNotificationService> logger, IMapper mapper,
    IAppContext appContext, IEventBus eventBus, ITemplateHelper templateHelper, IInAppNotificationRequestRepository inAppNotificationRequestRepository, IInAppNotificationTemplateRepository inAppNotificationTemplateRepository,
    IInAppNotificationRepository inAppNotificationRepository, IDateTimeService dateTimeService)
    : IInAppNotificationService
{
    private const int FailureReasonMaxLength = 500;
    private const string NotificationIdKey = "inAppNotificationId";

    /// <inheritdoc />
    public async Task<PagedResponse<InAppNotificationDto>> GetInAppNotifications(GetInAppNotificationRequest getInAppNotificationRequest)
    {
        var inAppNotifications = await inAppNotificationRepository.GetInAppNotifications(appContext.UserId, getInAppNotificationRequest);

        var pagedResponse = new PagedResponse<InAppNotificationDto>
        {
            Items = mapper.Map<List<InAppNotificationDto>>(inAppNotifications.Items),
            ContinuationToken = inAppNotifications.ContinuationToken,
        };
        return pagedResponse;
    }

    /// <inheritdoc />
    public async Task<string> AddInAppNotificationRequestAsync(InAppNotificationRequestDto inAppNotificationRequestDto)
    {
        ErrorCode? errorCode;
        string errorMessage;
        var inAppNotificationRequest = mapper.Map<InAppNotificationRequest>(inAppNotificationRequestDto);

        // Fetch in app notification template & validate it.
        var inAppNotificationTemplate = await this.GetInAppNotificationTemplateAsync(inAppNotificationRequestDto);

        if (!ValidateInAppNotificationRequest(inAppNotificationRequest, inAppNotificationTemplate, out errorCode, out errorMessage))
        {
            throw new Exceptions.ApplicationException((ErrorCode)errorCode, errorMessage);
        }

        // Save the in app notifications to the database
        inAppNotificationRequest.Status = NotificationStatus.Queued;
        inAppNotificationRequest.TemplateId = inAppNotificationTemplate.InAppNotificationTemplateId;
        var result = await inAppNotificationRequestRepository.SaveInAppNotificationRequestAsync(inAppNotificationRequest);

        // Publish the in app notification event to the event bus for processing in the background
        var inAppNotificationEvent = new InAppNotificationRequestReceivedEvent(appContext.TenantId, appContext.UserId, result.requestId, result.partitionKey);
        await eventBus.PublishAsync(inAppNotificationEvent);

        return result.requestId;
    }

    /// <inheritdoc />
    public async Task EnrichInAppNotificationRequestAsync(string inAppNotificationRequestId, string partitionKey)
    {
        InAppNotificationRequest inAppNotificationRequest = null;
        try
        {
            // Get in app notification request and validate.
            inAppNotificationRequest = await this.GetInAppNotificationRequestAsync(inAppNotificationRequestId, partitionKey);
            if (inAppNotificationRequest is null || inAppNotificationRequest.Status != NotificationStatus.Queued)
            {
                logger.LogInformation("Request with id {id} not found or already processed.", inAppNotificationRequestId);
                return;
            }

            // Get in app notification template.
            var inAppNotificationTemplate = await inAppNotificationTemplateRepository.GetInAppNotificationTemplateByIdAsync(inAppNotificationRequest.TemplateId);

            // Convert request into in-app notifications.
            var inAppNotifications = this.BuildInAppNotifications(inAppNotificationRequest, inAppNotificationTemplate);

            // Insert in app notification records.
            if (inAppNotifications.Count > 0)
            {
                var inAppNotificationIds = await inAppNotificationRepository.AddInAppNotifications(inAppNotifications);
                await this.PublishInAppSendEventsAsync(inAppNotificationIds);
            }

            // Update status
            await this.UpdateNotificationRequestStatus(inAppNotificationRequest, partitionKey, NotificationStatus.Enriched);
        }
        catch (Exceptions.ApplicationException ex)
        {
            logger.LogError(ex, "Failed to build in app notification for {requestId}: {exception}", inAppNotificationRequestId, ex.Message);
            if (inAppNotificationRequest != null)
            {
                await this.UpdateNotificationRequestStatus(inAppNotificationRequest, partitionKey, NotificationStatus.Failed, ex.Message);
            }
        }
    }

    /// <inheritdoc />
    public async Task<bool> MarkNotificationAsReadAsync(string inAppNotificationId)
    {
        return await inAppNotificationRepository.MarkNotificationAsRead(appContext.UserId, inAppNotificationId);
    }

    /// <inheritdoc />
    public async Task<bool> MarkAllNotificationsAsReadAsync()
    {
        return await inAppNotificationRepository.MarkAllNotificationsAsRead(appContext.UserId);
    }

    private static bool ValidateInAppNotificationRequest(InAppNotificationRequest inAppNotificationRequest, InAppNotificationTemplate inAppNotificationTemplate, out ErrorCode? errorCode, out string errorMessage)
    {
        errorCode = null;
        errorMessage = null;

        if (inAppNotificationTemplate is null)
        {
            errorCode = ErrorCode.RECORD_NOT_FOUND;
            errorMessage = $"Template not found with given id or name.";
            return false;
        }

        var isPersonalizationValid = inAppNotificationRequest.Personalization.TrueForAll(personalization => personalization.TemplateData.HasKeys(inAppNotificationTemplate.Parameters));
        if (!isPersonalizationValid)
        {
            errorCode = ErrorCode.INVALID_ARGS;
            errorMessage = "Personalization doesn't have required template data.";
            return false;
        }

        return true;
    }

    private async Task<InAppNotificationRequest> GetInAppNotificationRequestAsync(string inAppNotificationRequestId, string partitionKey)
    {
        return await inAppNotificationRequestRepository.GetInAppNotificationRequestAsync(inAppNotificationRequestId, partitionKey);
    }

    private async Task<InAppNotificationTemplate> GetInAppNotificationTemplateAsync(InAppNotificationRequestDto inAppNotificationDto)
    {
        InAppNotificationTemplate inAppNotificationTemplate = null;
        if (inAppNotificationDto.TemplateId.HasValue)
        {
            inAppNotificationTemplate = await inAppNotificationTemplateRepository.GetInAppNotificationTemplateByIdAsync(inAppNotificationDto.TemplateId.Value);
        }
        else if (!string.IsNullOrWhiteSpace(inAppNotificationDto.TemplateName))
        {
            inAppNotificationTemplate = await inAppNotificationTemplateRepository.GetInAppNotificationTemplateByNameAsync(inAppNotificationDto.TemplateName);
        }

        return inAppNotificationTemplate;
    }

    private List<InAppNotification> BuildInAppNotifications(InAppNotificationRequest inAppNotificationRequest, InAppNotificationTemplate inAppNotificationTemplate)
    {
        var inAppNotifications = new List<InAppNotification>();
        HashSet<Guid> recipientsSet = new HashSet<Guid>();
        foreach (var personalization in inAppNotificationRequest.Personalization)
        {
            foreach (var recipient in personalization.Recipients.Distinct())
            {
                // Checking the condition to skip duplicate recipients.
                if (recipientsSet.Contains(recipient))
                {
                    continue;
                }

                // Build in app notification, if valid adding to stack.
                var inAppNotification = this.BuildInAppNotification(recipient, inAppNotificationTemplate, personalization);
                if (inAppNotification is not null)
                {
                    recipientsSet.Add(recipient);
                    inAppNotifications.Add(inAppNotification);
                }
            }
        }

        return inAppNotifications;
    }

    private InAppNotification BuildInAppNotification(Guid recipient, InAppNotificationTemplate inAppNotificationTemplate, InAppNotificationPersonalization inAppNotificationPersonalization)
    {
        var inAppNotification = new InAppNotification();
        this.MapTemplateData(inAppNotification, inAppNotificationTemplate, inAppNotificationPersonalization.TemplateData);
        if (string.IsNullOrWhiteSpace(inAppNotification.Title) ||
            string.IsNullOrWhiteSpace(inAppNotification.Body))
        {
            return null;
        }

        // Set properties
        inAppNotification.EmployeeId = recipient;
        inAppNotification.InAppNotificationId = dateTimeService.GetInvertedTicks();
        inAppNotification.MetaData = inAppNotificationPersonalization.MetaData ?? new Dictionary<string, object>();
        inAppNotification.MetaData.Add(NotificationIdKey, inAppNotification.InAppNotificationId);
        inAppNotification.Url = inAppNotificationPersonalization.Url;

        // Substitute URL
        if (!string.IsNullOrWhiteSpace(inAppNotification.Url))
        {
            inAppNotification.Url = templateHelper.ReplacePlaceholders(inAppNotification.Url, inAppNotification.MetaData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString()));
        }

        return inAppNotification;
    }

    private void MapTemplateData(InAppNotification inAppNotification, InAppNotificationTemplate inAppNotificationTemplate, Dictionary<string, string> dynamicTemplateData)
    {
        if (!string.IsNullOrWhiteSpace(inAppNotificationTemplate.Body))
        {
            inAppNotification.Body = templateHelper.ReplacePlaceholders(inAppNotificationTemplate.Body, dynamicTemplateData);
            inAppNotification.Title = templateHelper.ReplacePlaceholders(inAppNotificationTemplate.Title, dynamicTemplateData);
        }
    }

    private async Task UpdateNotificationRequestStatus(InAppNotificationRequest request, string partitionKey, NotificationStatus status, string failureReason = null)
    {
        var inAppNotificationRequest = new InAppNotificationRequest()
        {
            InAppNotificationRequestId = request.InAppNotificationRequestId,
            Status = status,
            FailureReason = failureReason?.TrimToMaxLength(FailureReasonMaxLength) ?? failureReason,
        };
        await inAppNotificationRequestRepository.UpdateInAppNotificationRequestStatusAsync(inAppNotificationRequest, partitionKey);
    }

    private async Task PublishInAppSendEventsAsync(List<(Guid, string)> inAppNotifications)
    {
        foreach (var (employeeId, inAppNotificationId) in inAppNotifications)
        {
            // Raise event to send email
            var sendInAppNotificationRequest = new SendInAppNotificationEvent(appContext.TenantId, appContext.UserId, employeeId, inAppNotificationId);
            await eventBus.PublishAsync(sendInAppNotificationRequest);
        }
    }
}