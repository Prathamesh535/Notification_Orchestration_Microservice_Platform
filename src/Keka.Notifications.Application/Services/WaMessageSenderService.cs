// -----------------------------------------------------------------------
// <copyright file="WaMessageSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Initializes a new instance of the <see cref="WaMessageSenderService"/> class.
/// </summary>
/// <param name="logger">The logger instance.</param>
/// <param name="mapper">The mapper instance.</param>
/// <param name="unitOfWork">The unit of work instance.</param>
/// <param name="waMessageSender">The whatsapp message sender instance.</param>
/// <param name="waMessageRepository">The whatsapp message repository.</param>
public class WaMessageSenderService(ILogger<WaMessageSenderService> logger, IMapper mapper, IUnitOfWork unitOfWork,
                                    IWaMessageSender waMessageSender, IWaMessageRepository waMessageRepository)
    : BaseService(logger, mapper, unitOfWork), IWaMessageSenderService
{
    /// <inheritdoc/>
    public async Task SendWaMessageAsync(SendWaMessageEvent sendWaMessageEvent)
    {
        WaMessage waMessage = null;
        try
        {
            // Retrieve WhatsApp message request.
            waMessage = await waMessageRepository.GetWaMessageAsync(sendWaMessageEvent.WaMessageId);
            if (waMessage is null)
            {
                this.Logger.LogInformation("WaMessage record is not found with given Id {id}", sendWaMessageEvent.WaMessageId);
                return;
            }

            // Build message request
            SendWaMessageRequest messageRequest = BuildWaSendMessageRequest(waMessage);

            // Send WhatsApp message.
            var response = await waMessageSender.SendAsync(messageRequest);

            // Update WhatsApp message status based on the response
            await this.UpdateWaMessageStatusAsync(waMessage, response, sendWaMessageEvent.WaMessageId);
        }
        catch (Exception ex)
        {
            string errorMessage = $"{ex.Message ?? "Unknown error"}";
            this.Logger.LogError(ex, "{errorMessage}", errorMessage);

            // Update Wa status as failed
            if (waMessage is not null)
            {
                var sendSmsResponse = new SendWaMessageResponse() { ErrorMessage = errorMessage, Success = false };
                await this.UpdateWaMessageStatusAsync(waMessage, sendSmsResponse, sendWaMessageEvent.WaMessageId);
            }
        }
    }

    private static SendWaMessageRequest BuildWaSendMessageRequest(WaMessage waMessage)
    {
        List<Abstractions.WA.WaRecipientList> recipients = BuildWaRecipientList(waMessage.Personalization, waMessage.ParameterMapping);

        return new SendWaMessageRequest()
        {
            FromPhoneNumber = waMessage.FromPhoneNumber,
            TemplateName = waMessage.ExternalTemplateId,
            TemplateLanguage = waMessage.Language,
            Recipients = recipients,
        };
    }

    private static List<WaRecipientList> BuildWaRecipientList(List<WaPersonalization> recipientList, Dictionary<string, string> parameterMapping)
    {
        return recipientList.Select(x =>
        {
            var waRecipientList = new WaRecipientList()
            {
                PhoneNumbers = x.Recipients,
                TemplateData = BuildTemplateData(parameterMapping, x.TemplateData),
            };
            if (x.Attachments is not null)
            {
                waRecipientList.Attachments = x.Attachments.Select(MapToWaAttachment).ToList();
            }

            return waRecipientList;
        }).ToList();
    }

    private static Abstractions.WA.WaAttachment MapToWaAttachment(Core.Models.Wa.WaAttachment waAttachment)
    {
        return new Abstractions.WA.WaAttachment
        {
            Content = waAttachment.Content,
            Url = waAttachment.Url,
            FileName = waAttachment.FileName,
            Type = (Abstractions.WA.WaAttachmentType)waAttachment.Type,
        };
    }

    private static Dictionary<string, string> BuildTemplateData(Dictionary<string, string> parameterMapping, Dictionary<string, string> templateData)
    {
        return templateData.Where(kvp => parameterMapping.ContainsKey(kvp.Key))
                             .ToDictionary(kvp => parameterMapping[kvp.Key], kvp => kvp.Value);
    }

    private async Task UpdateWaMessageStatusAsync(WaMessage waMessage, SendWaMessageResponse sendWaMessageResponse, string waMessageId)
    {
        waMessage.Status = sendWaMessageResponse.Success ? NotificationStatus.Sent : NotificationStatus.Failed;
        waMessage.ExternalId = sendWaMessageResponse.ReferenceId;
        waMessage.FailureReason = sendWaMessageResponse.ErrorMessage;

        await waMessageRepository.UpdateWaMessageAsync(waMessageId, waMessage);
    }
}
