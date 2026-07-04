// -----------------------------------------------------------------------
// <copyright file="SlackNotificationSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Slack notification sender service.
/// </summary>
/// <param name="logger">The logger instance.</param>
/// <param name="slackProvider">Provider instance from slack package.</param>
/// <param name="slackNotificationTemplateRepository">The SlackNotificationTemplateRepository instance.</param>
/// <param name="slackRequestRepository">The SlackRequestRepository instance.</param>
public class SlackNotificationSenderService(ILogger<EmailService> logger, ISlackProvider slackProvider, ISlackRequestRepository slackRequestRepository, ISlackNotificationTemplateRepository slackNotificationTemplateRepository)
: ISlackNotificationSenderService
{
    /// <inheritdoc/>
    public async Task SendSlackNotificationAsync(SendSlackNotificationEvent sendSlackNotificationEvent)
    {
        // Fetch slack notification request.
        var slackNotificationRequest = await slackRequestRepository.GetSlackNotificationRequestAsync(sendSlackNotificationEvent.SlackRequestId, sendSlackNotificationEvent.PartitionKey);
        if (slackNotificationRequest is null)
        {
            logger.LogInformation("Slack notification Record is not found with given Id {SlackRequestId}", sendSlackNotificationEvent.SlackRequestId);
            return;
        }

        // Fetch template from db.
        var template = await slackNotificationTemplateRepository.GetSlackNotificationTemplateAsync(slackNotificationRequest.TemplateId);
        if (template is null || string.IsNullOrEmpty(template.Body))
        {
            logger.LogError("No template assosiated to given Id/name.");
            return;
        }

        // Enrich template.
        string message = Handlebars.Compile(template.Body).Invoke(slackNotificationRequest.Payload);

        try
        {
            // Send request.
            var response = await slackProvider.PostMessageAsync(slackNotificationRequest.Url, message.FromJson<Keka.Notifications.Abstractions.Slack.Models.SlackMessageRequest>());

            if (!response.Success)
            {
                throw new Exceptions.ApplicationException(ErrorCode.UNKNOWN, $"Sending slack notification failed {response.ErrorMessage}, referenceId: {response.ReferenceId}");
            }

            // Update response and status.
            await this.UpdateSlackNotificationAsync(slackNotificationRequest, sendSlackNotificationEvent.PartitionKey, NotificationStatus.Sent, response.ToJson(), true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{errorMessage}", ex.Message);
            await this.UpdateSlackNotificationAsync(slackNotificationRequest, sendSlackNotificationEvent.PartitionKey, NotificationStatus.Failed, ex.Message, false);
        }
    }

    private async Task UpdateSlackNotificationAsync(SlackNotificationRequest slackNotificationRequest, string partitionKey, NotificationStatus status, string response, bool isSuccess = true)
    {
        if (isSuccess)
        {
            slackNotificationRequest.Payload = string.Empty;
        }

        slackNotificationRequest.NotificationStatus = (int)status;
        slackNotificationRequest.RawResponse = response;
        slackNotificationRequest.HasException = !isSuccess;
        await slackRequestRepository.UpdateSlackNotificationAsync(slackNotificationRequest, partitionKey);
    }
}
