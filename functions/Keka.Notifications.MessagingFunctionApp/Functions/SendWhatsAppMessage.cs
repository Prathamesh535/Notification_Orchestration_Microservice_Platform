// -----------------------------------------------------------------------
// <copyright file="SendWhatsAppMessage.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.MessagingFunctionApp.Functions;

/// <summary>
/// Initializes a new instance of the <see cref="SendWhatsAppMessage"/> class.
/// </summary>
/// <param name="rootScope">The root scope.</param>
/// <param name="logger">The logger service.</param>
/// <param name="waMessageSenderService">The whatsapp message sender service.</param>
public class SendWhatsAppMessage(ILifetimeScope rootScope, ILogger<SendWhatsAppMessage> logger,
                                 IWaMessageSenderService waMessageSenderService)
{
    [Function(nameof(SendWhatsAppMessage))]
    public async Task RunAsync([ServiceBusTrigger("keka_event_bus", "keka_notifications_whatsapp", Connection = "EventBus")] string message)
    {
        SendWaMessageEvent sendWaMessageEvent;
        if (!message.TryDeserialize(out sendWaMessageEvent))
        {
            logger.LogError("Invalid message: {message}", message);
            return;
        }
        try
        {
            rootScope.Resolve<IAppContext>().TenantId = sendWaMessageEvent.TenantId;
            rootScope.Resolve<IAppContext>().UserId = sendWaMessageEvent.UserId;

            await waMessageSenderService.SendWaMessageAsync(sendWaMessageEvent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending whatsapp message for message id: {messageId}", sendWaMessageEvent.WaMessageId);
        }
    }
}