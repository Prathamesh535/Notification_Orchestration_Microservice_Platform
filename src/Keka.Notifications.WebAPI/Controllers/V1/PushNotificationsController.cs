// -----------------------------------------------------------------------
// <copyright file="PushNotificationsController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.V1;

/// <summary>
/// Represents the rest endpoints related to push notifications.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/push-notifications")]
public class PushNotificationsController : BaseApiController
{
    private readonly IPushNotificationRequestService pushNotificationRequestService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PushNotificationsController"/> class.
    /// </summary>
    /// <param name="pushNotificationRequestService">The push notification request service.</param>
    public PushNotificationsController(IPushNotificationRequestService pushNotificationRequestService)
    {
        this.pushNotificationRequestService = pushNotificationRequestService;
    }

    /// <summary>
    /// Adds a push notification request.
    /// </summary>
    /// <param name="pushNotificationRequest">The push notification.</param>
    /// <returns>Returns IActionResult indicating the id of the push notification request inserted.</returns>
    [ClientCredentialsOnly]
    [HttpPost("send")]
    [AllowOnlyIfEnabled(NotificationType.Push)]
    public async Task<IActionResult> SendPushNotification([FromBody] PushNotificationRequestDto pushNotificationRequest)
    {
        var pushNotificationRequestId = await this.pushNotificationRequestService.AddPushNotificationRequest(pushNotificationRequest);
        return this.ToOkResponse(pushNotificationRequestId);
    }
}
