// -----------------------------------------------------------------------
// <copyright file="SendSms.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.MessagingFunctionApp.Functions;

/// <summary>
/// Handles the sending of SMS messages via a service bus trigger.
/// </summary>
public class SendSms
{
    private readonly ILifetimeScope rootScope;
    private readonly ILogger<SendSms> logger;
    private readonly ISmsSenderService smsSenderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendSms"/> class.
    /// </summary>
    /// <param name="rootScope">The root scope.</param>
    /// <param name="logger">The logger service.</param>
    /// <param name="smsSenderService">The SMS sender service.</param>
    public SendSms(ILifetimeScope rootScope, ILogger<SendSms> logger, ISmsSenderService smsSenderService)
    {
        this.rootScope = rootScope;
        this.logger = logger;
        this.smsSenderService = smsSenderService;
    }

    /// <summary>
    /// Processes messages from the service bus and sends SMS.
    /// </summary>
    /// <param name="message">The message from the service bus.</param>
    [Function(nameof(SendSms))]
    public async Task RunAsync([ServiceBusTrigger("keka_event_bus", "keka_notifications_sms", Connection = "EventBus")] string message)
    {
        SendSmsEvent sendSmsEvent;
        if (!message.TryDeserialize(out sendSmsEvent))
        {
            logger.LogError("Invalid message: {message}", message);
            return;
        }

        try
        {
            rootScope.Resolve<IAppContext>().TenantId = sendSmsEvent.TenantId;
            rootScope.Resolve<IAppContext>().UserId = sendSmsEvent.UserId;
            await smsSenderService.SendSmsAsync(sendSmsEvent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending sms Request for Request id: {messageId}", sendSmsEvent.SmsRequestId);
        }
    }
}
