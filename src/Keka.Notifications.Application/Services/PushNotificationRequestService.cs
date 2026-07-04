// -----------------------------------------------------------------------
// <copyright file="PushNotificationRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Push notification request service.
/// </summary>
/// <param name="mapper">Th emapper instance.</param>
/// <param name="pushNotificationRequestRepository">The PushNotificationRequestRepository instance.</param>
/// <param name="eventBus">The event bus instance.</param>
/// <param name="appContext">The appContext instance.</param>
/// <param name="pushNotificationTemplateRepository">The PushNotificationTemplateRepository instance.</param>
public class PushNotificationRequestService(IMapper mapper, IPushNotificationRequestRepository pushNotificationRequestRepository, IEventBus eventBus, IAppContext appContext, IPushNotificationTemplateRepository pushNotificationTemplateRepository)
    : IPushNotificationRequestService
{
    /// <inheritdoc/>
    public async Task<Guid> AddPushNotificationRequest(PushNotificationRequestDto pushNotificationRequestDto)
    {
        ErrorCode? errorCode;
        string errorMessage;

        // Getting respective Push Notification Template from DataBase.
        PushNotificationTemplate pushNotificationTemplate = await this.GetPushNotificationTemplateAsync(pushNotificationRequestDto);

        // Map smsRequestDto to smsRequest using AutoMapper
        var pushNotificationRequest = mapper.Map<PushNotificationRequest>(pushNotificationRequestDto);
        if (pushNotificationTemplate == null)
        {
            throw new Exceptions.ApplicationException(ErrorCode.RECORD_NOT_FOUND, "Push notification template record not found with given id");
        }

        if (!ValidatePushNotificationRequest(pushNotificationRequest, pushNotificationTemplate, out errorCode, out errorMessage))
        {
            throw new Exceptions.ApplicationException((ErrorCode)errorCode, errorMessage);
        }

        // Save the push notification request to the database
        pushNotificationRequest.PushNotificationTemplateId = pushNotificationTemplate.PushNotificationTemplateId;
        var result = await pushNotificationRequestRepository.InsertPushNotificationRequestAsync(pushNotificationRequest);

        // Publish an event using EventBus to send push notification
        await eventBus.PublishAsync(new SendPushNotificationEvent(appContext.TenantId, appContext.UserId, result.requestId, result.partitionKey));
        return result.requestId;
    }

    private static bool ValidatePushNotificationRequest(PushNotificationRequest pushNotificationRequest, PushNotificationTemplate pushNotificationTemplate, out ErrorCode? errorCode, out string errorMessage)
    {
        errorCode = null;
        errorMessage = null;

        foreach (var personalizationItem in pushNotificationRequest.Personalization)
        {
            bool allParametersPresent = pushNotificationTemplate.TemplateParameters.TrueForAll(key => personalizationItem.TemplateData.ContainsKey(key));

            if (!allParametersPresent)
            {
                errorCode = ErrorCode.INVALID_ARGS;
                errorMessage = "One or more arguments are missing in personalization";
                return false;
            }
        }

        return true;
    }

    private async Task<PushNotificationTemplate> GetPushNotificationTemplateAsync(PushNotificationRequestDto pushNotificationRequestDto)
    {
        PushNotificationTemplate pushNotificationTemplate = null;
        if (pushNotificationRequestDto.PushNotificationTemplateId.HasValue)
        {
            pushNotificationTemplate = await pushNotificationTemplateRepository.GetPushNotificationTemplateByIdAsync(pushNotificationRequestDto.PushNotificationTemplateId.Value);
        }
        else if (!string.IsNullOrWhiteSpace(pushNotificationRequestDto.PushNotificationTemplateName))
        {
            pushNotificationTemplate = await pushNotificationTemplateRepository.GetPushNotificationTemplateByNameAsync(pushNotificationRequestDto.PushNotificationTemplateName);
        }

        return pushNotificationTemplate;
    }
}
