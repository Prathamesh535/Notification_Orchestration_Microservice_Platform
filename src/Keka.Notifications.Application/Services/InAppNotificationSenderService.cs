// -----------------------------------------------------------------------
// <copyright file="InAppNotificationSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents an in app notification sender service.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="webPushNotificationProvider">The we push notification provider.</param>
/// <param name="inAppNotificationRepository">The InAppNotificationRepository instance.</param>
public class InAppNotificationSenderService(ILogger<InAppNotificationSenderService> logger, IMapper mapper, IWebPushNotificationProvider webPushNotificationProvider, IInAppNotificationRepository inAppNotificationRepository)
    : IInAppNotificationSenderService
{
    /// <inheritdoc />
    public async Task SendInAppNotificationWebPushAsync(SendInAppNotificationEvent sendInAppNotificationEvent)
    {
        try
        {
            // Get in app notification.
            var inAppNotification = await inAppNotificationRepository.GetInAppNotification(sendInAppNotificationEvent.EmployeeId, sendInAppNotificationEvent.InAppNotificationId);

            // Build notification request
            var sendWebPushNotificationRequest = mapper.Map<SendWebPushNotificationRequest>(inAppNotification);

            // Send
            var response = await webPushNotificationProvider.SendAsync(sendWebPushNotificationRequest);
            if (response is null || !response.Success)
            {
                logger.LogError("Failed to send web push notification for {id}: {response}", sendInAppNotificationEvent.InAppNotificationId, JsonSerializer.Serialize(response));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while sending web push for id {id}: {message}", sendInAppNotificationEvent.InAppNotificationId, ex.Message);
        }
    }
}