// -----------------------------------------------------------------------
// <copyright file="EmployeeEmailUpdatedEventHandler.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker.EventHandlers;

/// <summary>
/// Represents the employee email updated event handler.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="emailStatusService">The email service.</param>
public class EmployeeEmailUpdatedEventHandler(ILogger<EmployeeEmailUpdatedEventHandler> logger, IEmailStatusService emailStatusService)
    : IEventHandler<EmployeeEmailUpdatedEvent>
{
    private readonly ILogger<EmployeeEmailUpdatedEventHandler> logger = logger;
    private readonly IEmailStatusService emailStatusService = emailStatusService;

    /// <inheritdoc/>
    public async Task Handle(EmployeeEmailUpdatedEvent @event)
    {
        try
        {
            await this.emailStatusService.UpdateEmailAsync(@event);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured while handling EmployeeUpdatedEvent {message}", ex.Message);
        }
    }
}