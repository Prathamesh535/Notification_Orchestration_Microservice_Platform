// -----------------------------------------------------------------------
// <copyright file="EmailTemplatesController.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI.Controllers.v1;

/// <summary>
/// Represents the email templates controller.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/emails/templates")]
public class EmailTemplatesController : BaseApiController
{
    private readonly IEmailTemplateService emailTemplateService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplatesController"/> class.
    /// </summary>
    /// <param name="emailTemplateService">The email template service.</param>
    public EmailTemplatesController(IEmailTemplateService emailTemplateService)
    {
        this.emailTemplateService = emailTemplateService;
    }

    /// <summary>
    /// Get email template by template identifier.
    /// </summary>
    /// <param name="emailTemplateId">The email template identifier.</param>
    /// <returns>Return the email template.</returns>
    [HttpGet("{emailTemplateId:guid}")]
    public async Task<IActionResult> GetTemplate(Guid emailTemplateId)
    {
        var emailTemplate = await this.emailTemplateService.GetTemplateAsync(emailTemplateId);
        return this.ToOkResponse(emailTemplate);
    }

    /// <summary>
    /// Create a new email template.
    /// </summary>
    /// <param name="emailTemplateCreateDto">The email template to create.</param>
    /// <returns>Return email template id.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateTemplate([FromBody] EmailTemplateCreateDto emailTemplateCreateDto)
    {
        var emailTemplateId = await this.emailTemplateService.CreateTemplateAsync(emailTemplateCreateDto);
        return this.ToOkResponse(emailTemplateId);
    }

    /// <summary>
    /// Update email template by template identifier.
    /// </summary>
    /// <param name="emailTemplateId">The email template id.</param>
    /// <param name="emailTemplateUpdateDto">The email template update dto.</param>
    /// <returns>Returns the asynchronous task result.</returns>
    [HttpPut("{emailTemplateId:guid}")]
    public async Task<IActionResult> UpdateTemplate(Guid emailTemplateId, [FromBody] EmailTemplateUpdateDto emailTemplateUpdateDto)
    {
        bool isSuccess = await this.emailTemplateService.UpdateTemplateAsync(emailTemplateId, emailTemplateUpdateDto);
        return this.ToOkResponse(isSuccess);
    }

    /// <summary>
    /// Delete email template by template identifier.
    /// </summary>
    /// <param name="emailTemplateId">The email template id.</param>
    /// <returns>Returns the asynchronous task result.</returns>
    [HttpDelete("{emailTemplateId:guid}")]
    public async Task<IActionResult> DeleteTemplate(Guid emailTemplateId)
    {
        bool isSuccess = await this.emailTemplateService.DeleteTemplateAsync(emailTemplateId);
        return this.ToOkResponse(isSuccess);
    }
}