// -----------------------------------------------------------------------
// <copyright file="EmailsController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.v1;

/// <summary>
/// Represents the rest endpoints to send emails.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/emails")]
public class EmailsController : BaseApiController
{
    private readonly IEmailService emailService;
    private readonly IEmailDeliveryService emailDeliveryService;
    private readonly IEmailStatusService emailStatusService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailsController"/> class.
    /// </summary>
    /// <param name="emailService">The email service.</param>
    /// <param name="emailStatusService">The email status service.</param>
    /// <param name="emailDeliveryService">The email delivery service.</param>
    public EmailsController(IEmailService emailService, IEmailStatusService emailStatusService, IEmailDeliveryService emailDeliveryService)
    {
        this.emailService = emailService;
        this.emailStatusService = emailStatusService;
        this.emailDeliveryService = emailDeliveryService;
    }

    /// <summary>
    /// Sends the email.
    /// </summary>
    /// <param name="emailRequest">The email message.</param>
    /// <returns>Returns Identifier of email request.</returns>
    [ClientCredentialsOnly]
    [HttpPost("send")]
    [AllowOnlyIfEnabled(NotificationType.Emails)]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto emailRequest)
    {
        Guid requestId = await this.emailService.AddEmailRequestAsync(emailRequest);
        return this.ToOkResponse<Guid>(requestId);
    }

    /// <summary>
    /// Unblocks given email.
    /// </summary>
    /// <param name="emailUnblockRequest">The email unblock request instance.</param>
    /// <returns>True if email is unblocked.</returns>
    [HttpPatch("unblock")]
    public async Task<IActionResult> UnblockEmail([FromBody] EmailUnblockRequest emailUnblockRequest)
    {
        return this.ToOkResponse(await this.emailStatusService.UnblockEmailAsync(emailUnblockRequest.Email));
    }

    /// <summary>
    /// Gets the email history records.
    /// </summary>
    /// <param name="getEmailHistoryRequest">The email history request.</param>
    /// <returns>Return request accepted result.</returns>
    [HttpGet("history")]
    public async Task<IActionResult> GetEmailHistory([FromQuery] GetEmailHistoryRequest getEmailHistoryRequest)
    {
        var result = await this.emailDeliveryService.GetEmailDeliveryHistoryAsync(getEmailHistoryRequest);
        return this.ToOkInfinitePagedResponse(result.Items, result.ContinuationToken);
    }

    /// <summary>
    /// Gets the raw data of email history records.
    /// </summary>
    /// <param name="id">The message id.</param>
    /// <returns>Return request accepted result.</returns>
    [HttpGet("history/{id}/rawData")]
    public async Task<IActionResult> GetEmailHistory(string id)
    {
         return this.ToOkResponse(await this.emailDeliveryService.GetEmailDeliveryDataByMessageIdAsync(id));
    }

    /// <summary>
    /// Get email status records.
    /// </summary>
    /// <param name="emailStatusRequest">Email status request.</param>
    /// <returns>Email status recrods.</returns>
    [HttpPost("status")]
    public async Task<IActionResult> GetEmailStatus(EmailStatusRequestDto emailStatusRequest)
    {
        var emailStatusRecords = await this.emailStatusService.GetEmailStatusRecordsAsync(emailStatusRequest);
        return this.ToOkResponse(emailStatusRecords);
    }

    /// <summary>
    /// Gets the blocked employee emails in past day.
    /// </summary>
    /// <returns> List of all blocked email status records in past one day.</returns>
    [HttpGet("blocked")]
    public async Task<IActionResult> GetBlockedEmailsInPastDay()
    {
        var blockedemailStatusRecordsInPastDay = await this.emailStatusService.GetBlockedEmailsInPastDay();
        return this.ToOkResponse(blockedemailStatusRecordsInPastDay);
    }
}