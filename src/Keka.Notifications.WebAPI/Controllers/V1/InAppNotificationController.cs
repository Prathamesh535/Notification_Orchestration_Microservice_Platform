// -----------------------------------------------------------------------
// <copyright file="InAppNotificationController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.v1;

/// <summary>
/// Represents the rest endpoints for in app notifications.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/in-app-notifications")]
public class InAppNotificationController : BaseApiController
{
    private readonly IInAppNotificationService inAppNotificationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="InAppNotificationController"/> class.
    /// </summary>
    /// <param name="inAppNotificationService">The in app notification service.</param>
    public InAppNotificationController(IInAppNotificationService inAppNotificationService)
    {
        this.inAppNotificationService = inAppNotificationService;
    }

    /// <summary>
    /// Gets the in app notifications.
    /// </summary>
    /// <param name="getInAppNotificationRequest">InAppNotification Request object.</param>
    /// <returns>Return the list of in app notifications.</returns>
    [HttpGet]
    public async Task<IActionResult> GetInAppNotifications([FromQuery] GetInAppNotificationRequest getInAppNotificationRequest)
    {
        var inAppNotifications = await this.inAppNotificationService.GetInAppNotifications(getInAppNotificationRequest);
        return this.ToOkResponse(inAppNotifications);
    }

    /// <summary>
    /// Sends the in app notification.
    /// </summary>
    /// <param name="inAppNotificationRequestDto">The in app notification request dto.</param>
    /// <returns>Return request accepted result.</returns>
    [ClientCredentialsOnly]
    [HttpPost("send")]
    [AllowOnlyIfEnabled(NotificationType.InApp)]
    public async Task<IActionResult> SendInAppNotification([FromBody] InAppNotificationRequestDto inAppNotificationRequestDto)
    {
        string requestId = await this.inAppNotificationService.AddInAppNotificationRequestAsync(inAppNotificationRequestDto);
        return this.ToOkResponse(requestId);
    }

    /// <summary>
    /// Marks the in app notification as read.
    /// </summary>
    /// <param name="inAppNotificationReadRequest">The in app notification read request.</param>
    /// <returns>Return boolean.</returns>
    [HttpPatch("read")]
    public async Task<IActionResult> MarkNotificationAsRead([FromBody] InAppNotificationReadRequest inAppNotificationReadRequest)
    {
        bool isRead = await this.inAppNotificationService.MarkNotificationAsReadAsync(inAppNotificationReadRequest.InAppNotificationId);
        return this.ToOkResponse(isRead);
    }

    /// <summary>
    /// Marks all in app notification as read.
    /// </summary>
    /// <returns>Return boolean.</returns>
    [HttpPatch("read-all")]
    public async Task<IActionResult> MarkAllNotificationsAsRead()
    {
        bool isSuccess = await this.inAppNotificationService.MarkAllNotificationsAsReadAsync();
        return this.ToOkResponse(isSuccess);
    }
}