// -----------------------------------------------------------------------
// <copyright file="CommonInfraModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Registrations;

/// <summary>
/// Represents the common infra autofac module.
/// </summary>
/// <seealso cref="Module" />
public class CommonInfraModule(IConfiguration configuration)
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
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();

        builder.RegisterType<DatabaseContext>()
            .WithParameter("connectionString", configuration.GetRequiredConnectionString("NotificationsDB"))
            .AsSelf()
            .InstancePerLifetimeScope();

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new DateTimeTypeHandler());

        builder.RegisterType<DateTimeService>().As<IDateTimeService>().SingleInstance();
    }
}
