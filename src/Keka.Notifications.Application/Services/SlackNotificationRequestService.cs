// -----------------------------------------------------------------------
// <copyright file="SlackNotificationRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Slack notification request service.
/// </summary>
/// <param name="mapper">The mapper instance.</param>
/// <param name="appContext">App context.</param>
/// <param name="eventBus">Event bus.</param>
/// <param name="slackNotificationTemplateRepository">The slack notification template repository.</param>
/// <param name="slackRequestRepository">The slack request repository.</param>
public class SlackNotificationRequestService(IMapper mapper, IAppContext appContext, IEventBus eventBus, ISlackNotificationTemplateRepository slackNotificationTemplateRepository, ISlackRequestRepository slackRequestRepository)
: ISlackNotificationRequestService
{
    /// <inheritdoc/>
    public async Task<Guid> AddSlackNotificationRequestAsync(SlackNotificationRequestDto slackNotificationRequestDto)
    {
        // Validate request
        Guid? templateId = slackNotificationRequestDto.TemplateId;
        if (slackNotificationRequestDto.TemplateId is null && !string.IsNullOrWhiteSpace(slackNotificationRequestDto.TemplateName))
        {
            templateId = (await slackNotificationTemplateRepository.GetSlackNotificationTemplateByNameAsync(slackNotificationRequestDto.TemplateName))?.SlackTemplateId;
        }

        if (templateId is null || templateId.Value.Equals(Guid.Empty))
        {
            throw new Exceptions.ApplicationException(ErrorCode.INVALID_ARGS, "TemplateId or TemplatName is invalid.");
        }

        // Save request
        var slackRequest = mapper.Map<SlackNotificationRequest>(slackNotificationRequestDto);
        slackRequest.TemplateId = templateId.Value;
        var (slackRequestId, partitionKey) = await slackRequestRepository.SaveSlackNotificationRequestAsync(slackRequest);

        // Publish event to service bus
        await eventBus.PublishAsync(new SendSlackNotificationEvent(appContext.TenantId, appContext.UserId, slackRequestId, partitionKey));
        return slackRequestId;
    }
}
