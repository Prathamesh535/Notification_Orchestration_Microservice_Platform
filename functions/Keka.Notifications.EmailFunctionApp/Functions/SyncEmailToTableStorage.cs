namespace Keka.Notifications.EmailFunctionApp.Functions;

public class SyncEmailToTableStorage
{
    private readonly ILogger<SyncEmailToTableStorage> logger;
    private readonly IEmailDeliveryService emailDeliveryService;
    private readonly ILifetimeScope rootScope;

    /// <summary>
    /// Initializes a new instance of the <see cref="SyncEmailToTableStorage"/> class.
    /// </summary>
    /// <param name="rootScope">The root scope.</param>
    /// <param name="logger">The logger service.</param>
    /// <param name="emailDeliveryService">The email delivery service.</param>
    public SyncEmailToTableStorage(ILifetimeScope rootScope, ILogger<SyncEmailToTableStorage> logger, IEmailDeliveryService emailDeliveryService)
    {
        this.logger = logger;
        this.emailDeliveryService = emailDeliveryService;
        this.rootScope = rootScope;
    }

    [Function("SyncEmailToTable")]
    public async Task RunAsync([ServiceBusTrigger("keka_event_bus", "keka_notifications_sync_email", Connection = "EventBus")] string mySbMsg)
    {
        SyncEmailEvent syncEmailEvent;
        if (!mySbMsg.TryDeserialize(out syncEmailEvent))
        {
            this.logger.LogError("Invalid message: {message}", mySbMsg);
            return;
        }

        try
        {
            this.rootScope.Resolve<IAppContext>().TenantId = syncEmailEvent.TenantId;
            this.rootScope.Resolve<IAppContext>().UserId = syncEmailEvent.UserId;
            await emailDeliveryService.SyncEmailAsync(syncEmailEvent);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error publishing email to table for message id: {emailId}", syncEmailEvent.EmailId);
        }
    }
}