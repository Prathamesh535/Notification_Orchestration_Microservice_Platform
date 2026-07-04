// -----------------------------------------------------------------------
// <copyright file="EmailRequestReceivedEventHandler.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker.EventHandlers;

/// <summary>
/// Represents the email published event handler.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="emailService">The email service.</param>
public class EmailRequestReceivedEventHandler(ILogger<EmailRequestReceivedEventHandler> logger, IEmailService emailService)
    : IEventHandler<EmailRequestReceivedEvent>
{
    private readonly ILogger<EmailRequestReceivedEventHandler> logger = logger;
    private readonly IEmailService emailService = emailService;

    /// <inheritdoc/>
    public async Task Handle(EmailRequestReceivedEvent @event)
    {
        try
        {
            await this.emailService.EnrichEmailRequestAsync(@event.EmailRequestId);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured while handling EmailRequestReceivedEvent with request Id {requestId}. {message}", @event.EmailRequestId, ex.Message);
        }
    }
}