// -----------------------------------------------------------------------
// <copyright file="SlackController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.V1;

/// <summary>
/// Represents rest endpoints to send slack notifications.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/slack")]
public class SlackController : BaseApiController
{
    private readonly ISlackNotificationRequestService slackNotificationRequestService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SlackController"/> class.
    /// </summary>
    /// <param name="slackNotificationRequestService">the slack notification service.</param>
    public SlackController(ISlackNotificationRequestService slackNotificationRequestService)
    {
        this.slackNotificationRequestService = slackNotificationRequestService;
    }

    /// <summary>
    /// Saves the slack request.
    /// </summary>
    /// <param name="slackRequestDto">the slack notification endpoint and payload.</param>
    /// <returns>identifier of slack notification request.</returns>
    [ClientCredentialsOnly]
    [HttpPost("send")]
    [AllowOnlyIfEnabled(NotificationType.Slack)]
    public async Task<IActionResult> SaveSlackNotificationRequest([FromBody] SlackNotificationRequestDto slackRequestDto)
    {
        Guid requestId = await this.slackNotificationRequestService.AddSlackNotificationRequestAsync(slackRequestDto);
        return this.ToOkResponse<Guid>(requestId);
    }
}
