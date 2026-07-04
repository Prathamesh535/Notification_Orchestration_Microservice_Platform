// -----------------------------------------------------------------------
// <copyright file="MessagingFunctionAppModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application;

/// <summary>
/// Represents the messaging function app related autofac module.
/// </summary>
public class MessagingFunctionAppModule
 : Autofac.Module
{
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SmsRequestService>().As<ISmsRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<SmsSenderService>().As<ISmsSenderService>().InstancePerLifetimeScope();
        builder.RegisterType<WaMessageRequestService>().As<IWaMessageRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<WaMessageSenderService>().As<IWaMessageSenderService>().InstancePerLifetimeScope();
        builder.RegisterType<PushNotificationRequestService>().As<IPushNotificationRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<PushNotificationSenderService>().As<IPushNotificationSenderService>().InstancePerLifetimeScope();
        builder.RegisterType<WebhookNotificationSenderService>().As<IWebhookNotificationSenderService>().InstancePerLifetimeScope();
        builder.RegisterType<SlackNotificationRequestService>().As<ISlackNotificationRequestService>().InstancePerLifetimeScope();
        builder.RegisterType<SlackNotificationSenderService>().As<ISlackNotificationSenderService>().InstancePerLifetimeScope();
        builder.RegisterType<TemplateHelper>().As<ITemplateHelper>().InstancePerLifetimeScope();

        // Register the application context
        builder.Register(ctx => ctx.Resolve<IAppContextAccessor>().CurrentAppContext).As<IAppContext>().InstancePerLifetimeScope();
        builder.RegisterType<AppContextAccessor>().As<IAppContextAccessor>().InstancePerLifetimeScope();

        builder.RegisterType<InAppNotificationSenderService>().As<IInAppNotificationSenderService>().InstancePerLifetimeScope();
    }
}