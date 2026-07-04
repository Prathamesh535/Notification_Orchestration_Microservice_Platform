// -----------------------------------------------------------------------
// <copyright file="MessagingFunctionAppInfraModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Registrations;

/// <summary>
/// Represents the messaging function app autofac module.
/// </summary>
/// <seealso cref="Module" />
public class MessagingFunctionAppInfraModule()
    : Module
{
    /// <summary>
    /// Loads the module.
    /// </summary>
    /// <param name="builder">The autofac container builder.</param>
    /// <remarks>
    /// Note that the ContainerBuilder parameter is unique to this module.
    /// </remarks>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WaMessageRepository>().As<IWaMessageRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SmsRequestRepository>().As<ISmsRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SmsTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedSmsTemplateRepository<SmsTemplateRepository>>().As<ISmsTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PushNotificationRequestRepository>().As<IPushNotificationRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PushNotificationTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedPushNotificationTemplateRepository<PushNotificationTemplateRepository>>().As<IPushNotificationTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CountryRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedCountryRepository<CountryRepository>>().As<ICountryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<WaTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedWaTemplateRepository<WaTemplateRepository>>().As<IWaTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<InAppNotificationRepository>().As<IInAppNotificationRepository>().InstancePerLifetimeScope();
        builder.RegisterType<WebhookRequestRepository>().As<IWebhookRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SlackRequestRepository>().As<ISlackRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SlackNotificationTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedSlackNotificationTemplateRepository<SlackNotificationTemplateRepository>>().As<ISlackNotificationTemplateRepository>().InstancePerLifetimeScope();

        builder.Register(ctx =>
        {
            return new KekaHttpClient(new HttpClient(), new HttpClientOptions(), null, null, null);
        }).As<IHttpClient>().InstancePerLifetimeScope();
    }
}
