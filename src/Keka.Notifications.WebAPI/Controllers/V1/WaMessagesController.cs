// -----------------------------------------------------------------------
// <copyright file="WaMessagesController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.v1;

/// <summary>
/// Represents the rest endpoints to send whatsapp messages.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="WaMessagesController"/> class.
/// </remarks>
/// <param name="waMessageRequestService">The whatsapp message request service.</param>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/whatsapp")]
public class WaMessagesController(IWaMessageRequestService waMessageRequestService)
    : BaseApiController
{
    /// <summary>
    /// Sends the whatsapp message.
    /// </summary>
    /// <param name="waMessageRequest">The WhatsApp message request data.</param>
    /// <returns>Returns an HTTP OK response with the ID of the WhatsApp message request.</returns>
    [ClientCredentialsOnly]
    [HttpPost("send")]
    [AllowOnlyIfEnabled(NotificationType.Whatsapp)]
    public async Task<IActionResult> SendWhatsAppMessageAsyc([FromBody] WaMessageRequestDto waMessageRequest)
    {
        var waMessageRequestId = await waMessageRequestService.AddWaMessageRequestAsync(waMessageRequest);
        return this.ToOkResponse(waMessageRequestId);
    }
}