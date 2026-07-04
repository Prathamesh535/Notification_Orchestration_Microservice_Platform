// -----------------------------------------------------------------------
// <copyright file="WaMessageRequestService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Represents whatsapp message service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="WaMessageRequestService"/> class.
/// </remarks>
/// <param name="mapper">The mapper instance.</param>
/// <param name="appContext">The app context instance.</param>
/// <param name="eventBus">The event bus instance.</param>
/// <param name="countryRepository">The country repository.</param>
/// <param name="waTemplateRepository">The wa template repository.</param>
/// <param name="waMessageRepository">The wa message repository.</param>
public class WaMessageRequestService(IMapper mapper, IAppContext appContext, IEventBus eventBus, ICountryRepository countryRepository, IWaTemplateRepository waTemplateRepository, IWaMessageRepository waMessageRepository)
    : IWaMessageRequestService
{
    private const string DefaultCountryCode = "91";

    /// <inheritdoc/>
    public async Task<Guid> AddWaMessageRequestAsync(WaMessageRequestDto waMessageRequestDto)
    {
        ErrorCode? errorCode;
        string errorMessage;

        // Getting respective WhatsApp Template from Database.
        WaTemplate waTemplate = await this.GetWaTemplateAsync(waMessageRequestDto);

        // Map waMessageRequestDto to WaMessageRequest using AutoMapper
        var waMessageRequest = mapper.Map<WaMessageRequest>(waMessageRequestDto);

        // Fetch country information
        var country = await countryRepository.GetCountryByCodeAsync(DefaultCountryCode);

        // Validating WaMessage Request
        if (!ValidateWaMessageRequest(waMessageRequest, country, waTemplate, out errorCode, out errorMessage))
        {
            throw new Exceptions.ApplicationException((ErrorCode)errorCode, errorMessage);
        }

        // Save the WhatsApp message to the database
        var waMessage = BuildWaMessage(waMessageRequest, waTemplate, country);
        var waMessageId = await waMessageRepository.SaveWaMessageAsync(waMessage);

        // Publish an event using EventBus to send a WhatsApp message
        await eventBus.PublishAsync(new SendWaMessageEvent(appContext.TenantId, appContext.UserId, waMessageId));

        return waMessage.WaMessageId;
    }

    private static bool ValidateWaMessageRequest(WaMessageRequest waMessageRequest, Country country, WaTemplate waTemplate, out ErrorCode? errorCode, out string errorMessage)
    {
        errorCode = null;
        errorMessage = null;
        if (waTemplate is null)
        {
            errorCode = ErrorCode.RECORD_NOT_FOUND;
            errorMessage = $"Template not found with given id or name.";
            return false;
        }

        if (!country.IsWaEnabled)
        {
            errorCode = ErrorCode.COUNTRY_NOT_SUPPORTED;
            errorMessage = $"Whats App notifications are disabled in the country {country.Name}.";
            return false;
        }

        var templateKeys = waTemplate.ParameterMapping.Keys.ToList();
        foreach (var recipient in waMessageRequest.Personalization)
        {
            if (recipient?.TemplateData is null)
            {
                continue;
            }

            bool isWaTemplateValid = recipient.TemplateData.HasKeys(templateKeys);
            if (!isWaTemplateValid)
            {
                errorCode = ErrorCode.INVALID_ARGS;
                errorMessage = $"TemplateData doesn't contain requried arguments.";
                return false;
            }
        }

        return true;
    }

    private static WaMessage BuildWaMessage(WaMessageRequest waMessageRequest, WaTemplate waTemplate, Country country)
    {
        var waMessage = new WaMessage();
        waMessage.FromPhoneNumber = country.FromPhoneNumber;
        waMessage.ExternalTemplateId = waTemplate.ExternalTemplateId;
        waMessage.Language = waTemplate.Language;
        waMessage.ParameterMapping = waTemplate.ParameterMapping;
        waMessage.Personalization = waMessageRequest.Personalization;
        waMessage.WaMessageId = Guid.NewGuid();
        waMessage.TemplateId = waMessageRequest.TemplateId;

        return waMessage;
    }

    private async Task<WaTemplate> GetWaTemplateAsync(WaMessageRequestDto waMessageRequestDto)
    {
        WaTemplate waTemplate = null;
        if (waMessageRequestDto.TemplateId.HasValue)
        {
            waTemplate = await waTemplateRepository.GetWaTemplateByIdAsync(waMessageRequestDto.TemplateId.Value);
        }
        else if (!string.IsNullOrWhiteSpace(waMessageRequestDto.TemplateName))
        {
            waTemplate = await waTemplateRepository.GetWaTemplateByNameAsync(waMessageRequestDto.TemplateName);
        }

        return waTemplate;
    }
}