// -----------------------------------------------------------------------
// <copyright file="SmsRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents Sms Request Service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SmsRequestService"/> class.
/// </remarks>
/// <param name="mapper">The mapper instance.</param>
/// <param name="eventBus">The event bus instance.</param>
/// <param name="appContext">The app context instance.</param>
/// <param name="smsRequestRepository">The sms request repository.</param>
/// <param name="countryRepository">The country repository.</param>
/// <param name="smsTemplateRepository">The sms template repository.</param>
public class SmsRequestService(IMapper mapper, IEventBus eventBus, IAppContext appContext, ISmsRequestRepository smsRequestRepository, ICountryRepository countryRepository, ISmsTemplateRepository smsTemplateRepository)
    : ISmsRequestService
{
    private const string DefaultCountryCode = "91";

    /// <inheritdoc/>
    public async Task<Guid> AddSmsRequestAsync(SmsRequestDto smsRequestDto)
    {
        ErrorCode? errorCode;
        string errorMessage;

        // Getting respective Sms Template from Database.
        SmsTemplate smsTemplate = await this.GetSmsTemplateAsync(smsRequestDto);

        // Map smsRequestDto to smsRequest using AutoMapper
        var smsRequest = mapper.Map<SmsRequest>(smsRequestDto);

        // Fetch country information
        var country = await countryRepository.GetCountryByCodeAsync(DefaultCountryCode);

        // Validating Sms Request
        if (!ValidateSmsRequest(smsRequest, country, smsTemplate, out errorCode, out errorMessage))
        {
            throw new Exceptions.ApplicationException((ErrorCode)errorCode, errorMessage);
        }

        // Save the SMS request to the database
        smsRequest.SmsTemplateId = smsTemplate.SmsTemplateId;
        var (smsRequestId, partitionKey) = await smsRequestRepository.SaveSmsRequestAsync(smsRequest);

        // Publish an event using EventBus to send an SMS
        await eventBus.PublishAsync(new SendSmsEvent(appContext.TenantId, appContext.UserId, smsRequestId, partitionKey));

        return smsRequestId;
    }

    private static bool ValidateSmsRequest(SmsRequest smsRequest, Country country, SmsTemplate smsTemplate, out ErrorCode? errorCode, out string errorMessage)
    {
        errorCode = null;
        errorMessage = null;

        if (smsTemplate is null)
        {
            errorCode = ErrorCode.RECORD_NOT_FOUND;
            errorMessage = $"Template not found with given id or name.";
            return false;
        }

        if (!country.IsSmsEnabled)
        {
            errorCode = ErrorCode.COUNTRY_NOT_SUPPORTED;
            errorMessage = $"SMS notifications are disabled in the country {country.Name}";
            return false;
        }

        var templateKeys = smsTemplate.ParameterMapping.Keys.ToList();
        foreach (var recipient in smsRequest.Personalization)
        {
            if (recipient?.TemplateData is null)
            {
                continue;
            }

            bool isWaTemplateValid = recipient.TemplateData.HasKeys(templateKeys);
            if (!isWaTemplateValid)
            {
                errorCode = ErrorCode.INVALID_ARGS;
                errorMessage = $"SMS template has invalid arguments";
                return false;
            }
        }

        return true;
    }

    private async Task<SmsTemplate> GetSmsTemplateAsync(SmsRequestDto smsRequestDto)
    {
        SmsTemplate smsTemplate = null;
        if (smsRequestDto.SmsTemplateId.HasValue)
        {
            smsTemplate = await smsTemplateRepository.GetSmsTemplateByIdAsync(smsRequestDto.SmsTemplateId.Value);
        }
        else if (!string.IsNullOrWhiteSpace(smsRequestDto.SmsTemplateName))
        {
            smsTemplate = await smsTemplateRepository.GetSmsTemplateByNameAsync(smsRequestDto.SmsTemplateName);
        }

        return smsTemplate;
    }
}