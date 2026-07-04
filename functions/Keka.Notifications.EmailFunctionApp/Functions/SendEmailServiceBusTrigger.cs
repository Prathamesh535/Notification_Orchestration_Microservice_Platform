namespace Keka.Notifications.EmailFunctionApp.Functions;

public class SendEmailServiceBusTrigger
{
    private readonly ILifetimeScope rootScope;
    private readonly ILogger<SendEmailServiceBusTrigger> logger;
    private readonly IEmailSenderService emailSenderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendEmailServiceBusTrigger"/> class.
    /// </summary>
    /// <param name="rootScope">The root scope.</param>
    /// <param name="logger">The logger service.</param>
    /// <param name="emailSenderService">The email sender service.</param>
    public SendEmailServiceBusTrigger(ILifetimeScope rootScope, ILogger<SendEmailServiceBusTrigger> logger, IEmailSenderService emailSenderService)
    {
        this.rootScope = rootScope;
        this.logger = logger;
        this.emailSenderService = emailSenderService;
    }

    [Function("SendEmailUsingAmazonSES")]
    public async Task RunAsync([ServiceBusTrigger("keka_event_bus", "keka_notifications_email", Connection = "EventBus")] string mySbMsg)
    {
        SendEmailEvent sendEmailEvent;
        if (!mySbMsg.TryDeserialize(out sendEmailEvent))
        {
            this.logger.LogError("Invalid message: {message}", mySbMsg);
            return;
        }

        try
        {
            this.rootScope.Resolve<IAppContext>().TenantId = sendEmailEvent.TenantId;
            this.rootScope.Resolve<IAppContext>().UserId = sendEmailEvent.UserId;

            await emailSenderService.SendEmailAsync(sendEmailEvent);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error sending email for message id: {emailId}. Message : {message}", sendEmailEvent.EmailId, ex.Message);
            throw;
        }
    }
}