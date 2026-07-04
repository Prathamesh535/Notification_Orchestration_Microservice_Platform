// -----------------------------------------------------------------------
// <copyright file="SmsController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.v1;

/// <summary>
/// Represents the Sms controller.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SmsController"/> class.
/// </remarks>
/// <param name="smsRequestService">The sms Request service.</param>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sms")]
public class SmsController(ISmsRequestService smsRequestService)
    : BaseApiController
{
    /// <summary>
    /// Sends the sms Request.
    /// </summary>
    /// <param name="smsRequestDto">The Sms Request.</param>
    /// <returns>Returns Sms Request Id.</returns>
    [ClientCredentialsOnly]
    [HttpPost("send")]
    [AllowOnlyIfEnabled(NotificationType.Sms)]
    public async Task<IActionResult> SendSmsAsync([FromBody] SmsRequestDto smsRequestDto)
    {
        var smsRequestId = await smsRequestService.AddSmsRequestAsync(smsRequestDto);
        return this.ToOkResponse(smsRequestId);
    }
}
