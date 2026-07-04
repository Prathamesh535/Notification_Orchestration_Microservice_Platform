// -----------------------------------------------------------------------
// <copyright file="EmailTemplateService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents an email template service.
/// </summary>
public class EmailTemplateService : IEmailTemplateService
{
    private readonly IMapper mapper;
    private readonly IEmailTemplateRepository emailTemplateRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplateService"/> class.
    /// </summary>
    /// <param name="mapper">The mapper instance.</param>
    /// <param name="emailTemplateRepository">The email template repository instance.</param>
    public EmailTemplateService(IMapper mapper, IEmailTemplateRepository emailTemplateRepository)
    {
        this.mapper = mapper;
        this.emailTemplateRepository = emailTemplateRepository;
    }

    /// <inheritdoc />
    public async Task<EmailTemplateDto> GetTemplateAsync(Guid emailTemplateId)
    {
        var emailTemplate = await this.emailTemplateRepository.GetEmailTemplateAsync(emailTemplateId);
        var emailTemplateDto = this.mapper.Map<EmailTemplateDto>(emailTemplate);
        emailTemplateDto.TemplateName = emailTemplate.Name;
        return emailTemplateDto;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateTemplateAsync(EmailTemplateCreateDto emailTemplateCreateDto)
    {
        var emailTemplate = this.mapper.Map<EmailTemplate>(emailTemplateCreateDto);
        return await this.emailTemplateRepository.SaveEmailTemplateAsync(emailTemplate);
    }

    /// <inheritdoc />
    public async Task<bool> UpdateTemplateAsync(Guid emailTemplateId, EmailTemplateUpdateDto emailTemplateDto)
    {
        var emailTemplate = await this.emailTemplateRepository.GetEmailTemplateAsync(emailTemplateId);
        if (emailTemplate is null)
        {
            throw new Exceptions.ApplicationException(ErrorCode.RECORD_NOT_FOUND, $"Email template with id {emailTemplateId} not found.");
        }

        emailTemplate.Name = emailTemplateDto.Name;
        emailTemplate.Subject = emailTemplateDto.Subject;
        emailTemplate.Body = emailTemplateDto.Body;
        emailTemplate.From = emailTemplate.From;
        emailTemplate.ReplyTo = emailTemplate.ReplyTo;
        emailTemplate.EmailTemplateId = emailTemplateId;
        await this.emailTemplateRepository.UpdateEmailTemplateAsync(emailTemplate);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteTemplateAsync(Guid emailTemplateId)
    {
        var emailTemplate = await this.emailTemplateRepository.GetEmailTemplateAsync(emailTemplateId);
        if (emailTemplate is null)
        {
            throw new Exceptions.ApplicationException(ErrorCode.RECORD_NOT_FOUND, $"Email template with id {emailTemplateId} not found.");
        }

        await this.emailTemplateRepository.DeleteEmailTemplateAsync(emailTemplate);
        return true;
    }
}