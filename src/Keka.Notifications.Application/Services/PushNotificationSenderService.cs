// -----------------------------------------------------------------------
// <copyright file="PushNotificationSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents a push notification sender service.
/// </summary>
public class PushNotificationSenderService : IPushNotificationSenderService
{
    private readonly ITemplateHelper templateHelper;
    private readonly ILogger<PushNotificationSenderService> logger;
    private readonly IPushNotificationSender pushNotificationSender;
    private readonly IPushNotificationRequestRepository pushNotificationRequestRepository;
    private readonly IPushNotificationTemplateRepository pushNotificationTemplateRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PushNotificationSenderService"/> class.
    /// </summary>
    /// <param name="logger">The logger service.</param>
    /// <param name="pushNotificationSender">The push notification sender instance.</param>
    /// <param name="templateHelper">The template helper instance.</param>
    /// <param name="pushNotificationRequestRepository">The push notification request repository instance.</param>
    /// <param name="pushNotificationTemplateRepository">The push notification template repository instance.</param>
    public PushNotificationSenderService(ILogger<PushNotificationSenderService> logger, IPushNotificationRequestRepository pushNotificationRequestRepository, IPushNotificationSender pushNotificationSender, ITemplateHelper templateHelper, IPushNotificationTemplateRepository pushNotificationTemplateRepository)
    {
        this.logger = logger;
        this.pushNotificationSender = pushNotificationSender;
        this.templateHelper = templateHelper;
        this.pushNotificationRequestRepository = pushNotificationRequestRepository;
        this.pushNotificationTemplateRepository = pushNotificationTemplateRepository;
    }

    /// <inheritdoc/>
    public async Task SendPushNotificationAsync(SendPushNotificationEvent sendPushNotificationRequestEvent)
    {
        // Get push notification and validate it.
        var pushNotificationRequest = await this.pushNotificationRequestRepository.GetPushNotificationRequestAsync(sendPushNotificationRequestEvent.PushNotificationRequestId, sendPushNotificationRequestEvent.PartitionKey);
        if (pushNotificationRequest is null)
        {
            this.logger.LogInformation("Push notification Record is not found with given Id {PushNotificationRequestId}", sendPushNotificationRequestEvent.PushNotificationRequestId);
            return;
        }

        if (pushNotificationRequest.Status != NotificationStatus.Queued)
        {
            this.logger.LogInformation("PushNotificationRequest with Id {PushNotificationRequestId} is already Processed", sendPushNotificationRequestEvent.PushNotificationRequestId);
            return;
        }

        pushNotificationRequest.PushNotificationRequestId = sendPushNotificationRequestEvent.PushNotificationRequestId;
        if (pushNotificationRequest.Personalization.Count == 0)
        {
            throw new Exceptions.ApplicationException(ErrorCode.INVALID_ARGS, "Personalization data is empty for given {id}");
        }

        try
        {
            // Get push notification template and validate it.
            var pushNotificationTemplate = await this.pushNotificationTemplateRepository.GetPushNotificationTemplateByIdAsync(pushNotificationRequest.PushNotificationTemplateId);
            if (pushNotificationTemplate is null)
            {
                throw new Exceptions.ApplicationException(ErrorCode.RECORD_NOT_FOUND, "Push notification template not found with given id.");
            }

            // Merge recipients for similar template data and group the requests.
            var pushNotifications = GroupRecipientsByTemplate(pushNotificationRequest.Personalization);
            SendPushNotificationResponse pushNotificationSendResponse = new SendPushNotificationResponse() { Success = false };
            var multiDeviceNotifications = pushNotifications
                .Where(kvp => kvp.Recipients.Count > 1)
                .ToList();
            var deviceSpecificNotifications = pushNotifications
                .Where(kvp => kvp.Recipients.Count == 1)
                .ToList();

            // Sending notification
            foreach (var personalizationItem in multiDeviceNotifications)
            {
                PushNotificationTemplate template = this.MapDataToTemplate(pushNotificationTemplate, personalizationItem.TemplateData);
                pushNotificationSendResponse = await this.SendPushNotificationAsync(template, personalizationItem);
            }

            List<SendPushNotificationRequest> pushNotificationRequests = new List<SendPushNotificationRequest>();
            foreach (var personalizationItem in deviceSpecificNotifications)
            {
                PushNotificationTemplate template = this.MapDataToTemplate(pushNotificationTemplate, personalizationItem.TemplateData);
                pushNotificationRequests.Add(BuildPushNotificationRequest(template, personalizationItem));
            }

            if (pushNotificationRequests.Count > 0)
            {
                pushNotificationSendResponse = await this.pushNotificationSender.SendBulkAsync(pushNotificationRequests);
            }

            // Update push notification send status to db.
            await this.UpdatePushNotificationRequestStatusAsync(pushNotificationRequest, pushNotificationSendResponse);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured for RequestId:{PushNotificationRequestId} and PartitionKey:{PartitionKey}", sendPushNotificationRequestEvent.PushNotificationRequestId, sendPushNotificationRequestEvent.PartitionKey);

            // Update push notification failed status to db.
            await this.UpdatePushNotificationRequestStatusAsync(pushNotificationRequest, new SendPushNotificationResponse { Success = false, ErrorMessage = ex.Message });
        }
    }

    private static List<PushNotificationPersonalizationDto> GroupRecipientsByTemplate(List<PushNotificationPersonalization> personalizations)
    {
        var hashToPersonalizationMap = new Dictionary<int, PushNotificationPersonalizationDto>();

        foreach (var personalization in personalizations)
        {
            int hashCode = personalization.TemplateData.GetDataHashCode();

            if (hashToPersonalizationMap.TryGetValue(hashCode, out var existingPersonalization))
            {
                existingPersonalization.Recipients.AddRange(personalization.Recipients);
            }
            else
            {
                hashToPersonalizationMap[hashCode] = new PushNotificationPersonalizationDto
                {
                    Recipients = new List<string>(personalization.Recipients),
                    TemplateData = personalization.TemplateData,
                    Data = personalization.Data,
                };
            }
        }

        return hashToPersonalizationMap.Values.ToList();
    }

    private static SendPushNotificationRequest BuildPushNotificationRequest(PushNotificationTemplate pushNotificationTemplate, PushNotificationPersonalizationDto personalizationItem)
    {
        // Build push notification
        var sendPushNotificationRequest = new SendPushNotificationRequest()
        {
            Title = pushNotificationTemplate.Title,
            Body = pushNotificationTemplate.Body,
            DeviceTokens = personalizationItem.Recipients,
            Data = personalizationItem.Data,
        };
        return sendPushNotificationRequest;
    }

    private PushNotificationTemplate MapDataToTemplate(PushNotificationTemplate pushNotificationTemplate, Dictionary<string, string> templateData)
    {
        var template = new PushNotificationTemplate()
        {
            Title = this.templateHelper.ReplacePlaceholders(pushNotificationTemplate.Title, templateData),
            Body = this.templateHelper.ReplacePlaceholders(pushNotificationTemplate.Body, templateData),
            DataParameters = pushNotificationTemplate.DataParameters,
        };
        return template;
    }

    private async Task UpdatePushNotificationRequestStatusAsync(PushNotificationRequest pushNotificationRequest, SendPushNotificationResponse pushNotificationSendResponse)
    {
        NotificationStatus notificationStatus = pushNotificationSendResponse.Success ? NotificationStatus.Sent : NotificationStatus.Failed;
        pushNotificationRequest.Status = notificationStatus;
        pushNotificationRequest.FailureReason = pushNotificationSendResponse.ErrorMessage;
        pushNotificationRequest.ExternalId = pushNotificationSendResponse.ReferenceId;
        await this.pushNotificationRequestRepository.UpdatePushNotificationRequestStatusAsync(pushNotificationRequest);
    }

    private async Task<SendPushNotificationResponse> SendPushNotificationAsync(PushNotificationTemplate pushNotificationTemplate, PushNotificationPersonalizationDto personalizationItem)
    {
        // Build push notification
        var sendPushNotificationRequest = BuildPushNotificationRequest(pushNotificationTemplate, personalizationItem);

        // Send push notification
        return await this.pushNotificationSender.SendAsync(sendPushNotificationRequest);
    }
}