// -----------------------------------------------------------------------
// <copyright file="EmployeeAddedEventHandler.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker.EventHandlers;

/// <summary>
/// Represents the employee added event handler.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="emailStatusService">The email service.</param>
public class EmployeeAddedEventHandler(ILogger<EmployeeAddedEventHandler> logger, IEmailStatusService emailStatusService)
    : IEventHandler<EmployeeAddedEvent>
{
    private readonly ILogger<EmployeeAddedEventHandler> logger = logger;
    private readonly IEmailStatusService emailStatusService = emailStatusService;

    /// <inheritdoc/>
    public async Task Handle(EmployeeAddedEvent @event)
    {
        try
        {
            await this.emailStatusService.AddEmailStatusAsync(@event);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured while handling EmployeeAddedEvent {message}", ex.Message);
        }
    }
}