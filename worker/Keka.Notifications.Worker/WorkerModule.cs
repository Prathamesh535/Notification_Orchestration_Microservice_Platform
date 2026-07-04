// -----------------------------------------------------------------------
// <copyright file="WorkerModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker;

/// <summary>
/// Represents the core hr worker autofac module.
/// </summary>
public class WorkerModule : Autofac.Module
{
    /// <summary>
    /// Loads the module.
    /// </summary>
    /// <param name="builder">The autofac container builder.</param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx => ctx.Resolve<IAppContextAccessor>().CurrentAppContext).As<IAppContext>().InstancePerLifetimeScope();

        builder.RegisterType<AppContextAccessor>().As<IAppContextAccessor>().InstancePerLifetimeScope();

        builder.RegisterType<JobScheduler>().AsSelf().InstancePerLifetimeScope();

        var jobTypes = JobScheduler.GetTenantJobTypes();
        foreach (var type in jobTypes)
        {
            builder.RegisterType(type).AsSelf();
        }

        // Register the Autofac job activator
        builder.RegisterType<JobExecutor>().AsSelf().SingleInstance();
        builder.RegisterType<EmailRequestReceivedEventHandler>().As<IEventHandler<EmailRequestReceivedEvent>>().InstancePerDependency();

        builder.RegisterType<InAppNotificationRequestReceivedEventHandler>().As<IEventHandler<InAppNotificationRequestReceivedEvent>>().InstancePerDependency();
        builder.RegisterType<EmployeeAddedEventHandler>().As<IEventHandler<EmployeeAddedEvent>>().InstancePerDependency();
        builder.RegisterType<EmployeeEmailUpdatedEventHandler>().As<IEventHandler<EmployeeEmailUpdatedEvent>>().InstancePerDependency();
    }
}
