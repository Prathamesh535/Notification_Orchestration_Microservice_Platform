// -----------------------------------------------------------------------
// <copyright file="EmailFunctionAppModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application;

/// <summary>
/// Represents the email function app related autofac module.
/// </summary>
public class EmailFunctionAppModule
 : Autofac.Module
{
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailTemplateService>().As<IEmailTemplateService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailDeliveryService>().As<IEmailDeliveryService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailStatusService>().As<IEmailStatusService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailSenderService>().As<IEmailSenderService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailDeliveryConverter>().As<IEmailDeliveryConverter>().SingleInstance();
    }
}