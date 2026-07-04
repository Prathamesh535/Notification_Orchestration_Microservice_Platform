// -----------------------------------------------------------------------
// <copyright file="SmsSenderService.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Services;

/// <summary>
/// Initializes a new instance of the <see cref="SmsSenderService"/> class.
/// </summary>
/// <param name="logger">The Logger instance.</param>
/// <param name="smsSender">The Sms sender instance.</param>
/// <param name="smsRequestRepository">The sms request repository.</param>
/// <param name="smsTemplateRepository">The sms template repository.</param>
public class SmsSenderService(ILogger<SmsSenderService> logger, ISmsSender smsSender, ISmsRequestRepository smsRequestRepository, ISmsTemplateRepository smsTemplateRepository)
    : ISmsSenderService
{
    /// <summary>
    /// Method to send messages via Sms.
    /// </summary>
    /// <param name="sendSmsEvent">Sms Event.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SendSmsAsync(SendSmsEvent sendSmsEvent)
    {
        try
        {
            var smsRequest = await smsRequestRepository.GetSmsRequestAsync(sendSmsEvent.SmsRequestId, sendSmsEvent.PartitionKey);
            if (smsRequest is null)
            {
                logger.LogInformation("SmsRequest Record is not found with given Id {SmsRequestId}", sendSmsEvent.SmsRequestId);
                return;
            }

            if (smsRequest.Status != NotificationStatus.Queued)
            {
                logger.LogInformation("SmsRequest with Id {SmsRequestId} is already Processed.", sendSmsEvent.SmsRequestId);
                return;
            }

            var smsTemplate = await smsTemplateRepository.GetSmsTemplateByIdAsync((Guid)smsRequest.SmsTemplateId);
            var sendSmsRequest = BuildSendSmsRequest(smsRequest, smsTemplate);
            var smsSendResponse = await smsSender.SendAsync(sendSmsRequest);
            await this.UpdateSmsRequestStatusAsync(smsRequest, smsSendResponse, sendSmsEvent.PartitionKey);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while sending Sms with Sms RequestId: {id}", sendSmsEvent.SmsRequestId);

            // Update SMS status as failed in the database
            var sendSmsResponse = new SendSmsResponse { Success = false, ErrorMessage = ex.Message };
            var smsRequest = new SmsRequest { SmsRequestId = sendSmsEvent.SmsRequestId };
            await this.UpdateSmsRequestStatusAsync(smsRequest, sendSmsResponse, sendSmsEvent.PartitionKey);
        }
    }

    private static List<Abstractions.SMS.SmsRecipientList> BuildSmsRecipientList(List<SmsPersonalization> smsRecipientList, Dictionary<string, string> parameterMapping)
    {
        return smsRecipientList.Select(x =>
        {
            return new Abstractions.SMS.SmsRecipientList()
            {
                PhoneNumbers = x.Recipients,
                TemplateData = BuildTemplateData(parameterMapping, x.TemplateData),
            };
        }).ToList();
    }

    private static Dictionary<string, string> BuildTemplateData(Dictionary<string, string> parameterMapping, Dictionary<string, string> templateData)
    {
        return templateData.Where(kvp => parameterMapping.ContainsKey(kvp.Key))
                                   .ToDictionary(
                                       kvp => parameterMapping[kvp.Key],
                                       kvp => kvp.Value);
    }

    private static SendSmsRequest BuildSendSmsRequest(SmsRequest smsRequest, SmsTemplate smsTemplate)
    {
        List<Abstractions.SMS.SmsRecipientList> recipients = BuildSmsRecipientList(smsRequest.Personalization, smsTemplate.ParameterMapping);

        return new SendSmsRequest()
        {
            TemplateId = smsTemplate.ExternalTemplateId.ToString(),
            Recipients = recipients,
        };
    }

    private async Task UpdateSmsRequestStatusAsync(SmsRequest smsRequest, SendSmsResponse sendSmsResponse, string partitionKey)
    {
        smsRequest.Status = sendSmsResponse.Success ? NotificationStatus.Sent : NotificationStatus.Failed;
        smsRequest.ExternalId = sendSmsResponse.ReferenceId;
        smsRequest.FailureReason = sendSmsResponse.ErrorMessage;
        await smsRequestRepository.UpdateSmsRequestStatusAsync(smsRequest, partitionKey);
    }
}