// -----------------------------------------------------------------------
// <copyright file="WorkerAppModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application;

/// <summary>
/// Represents the worker app related autofac module.
/// </summary>
public class WorkerAppModule
 : Autofac.Module
{
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<InAppNotificationService>().As<IInAppNotificationService>().InstancePerLifetimeScope();

        builder.RegisterType<JobConfigService>().As<IJobConfigService>().InstancePerLifetimeScope();

        builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();

        builder.RegisterType<EmailTemplateService>().As<IEmailTemplateService>().InstancePerLifetimeScope();

        builder.RegisterType<EmailStatusService>().As<IEmailStatusService>().InstancePerLifetimeScope();
    }
}