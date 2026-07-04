// -----------------------------------------------------------------------
// <copyright file="WebhookController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.V1;

/// <summary>
/// Represents the webhook controller.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="WebhooksController"/> class.
/// </remarks>
/// <param name="webhookRequestService">the webhook request service.</param>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/webhooks")]
public class WebhooksController(IWebhookRequestService webhookRequestService)
    : BaseApiController
{
    private readonly IWebhookRequestService webhookRequestService = webhookRequestService;

    /// <summary>
    /// Saves the webhook request.
    /// </summary>
    /// <param name="webhookRequestDto">Represents Webhook request object.</param>
    /// <returns>Returns webhook request Id.</returns>
    [ClientCredentialsOnly]
    [HttpPost("send")]
    [AllowOnlyIfEnabled(NotificationType.Webhooks)]
    public async Task<IActionResult> SaveWebhookRequest([FromBody] WebhookRequestDto webhookRequestDto)
    {
        Guid webhookRequestId = await this.webhookRequestService.AddWebhookRequestAsync(webhookRequestDto);
        return this.ToOkResponse<Guid>(webhookRequestId);
    }
}
