// -----------------------------------------------------------------------
// <copyright file="ApiAppModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application;

/// <summary>
/// Represents the api application related autofac module.
/// </summary>
public class ApiAppModule : Autofac.Module
{
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<JobConfigService>().As<IJobConfigService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailSenderService>().As<IEmailSenderService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailTemplateService>().As<IEmailTemplateService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailDeliveryConverter>().As<IEmailDeliveryConverter>().InstancePerLifetimeScope();
        builder.RegisterType<EmailDeliveryService>().As<IEmailDeliveryService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailStatusService>().As<IEmailStatusService>().InstancePerLifetimeScope();
        builder.RegisterType<SmsRequestService>().As<ISmsRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<WaMessageRequestService>().As<IWaMessageRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<PushNotificationRequestService>().As<IPushNotificationRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<InAppNotificationService>().As<IInAppNotificationService>().InstancePerLifetimeScope();
        builder.RegisterType<EmployeeNotificationPreferenceService>().As<IEmployeeNotificationPreferenceService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailSenderService>().As<IEmailSenderService>().InstancePerLifetimeScope();
        builder.RegisterType<WebhookRequestService>().As<IWebhookRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<SlackNotificationRequestService>().As<ISlackNotificationRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<SlackNotificationSenderService>().As<ISlackNotificationSenderService>().InstancePerLifetimeScope();
    }
}