// -----------------------------------------------------------------------
// <copyright file="WebModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI;

/// <summary>
/// Represents the web API autofac module.
/// </summary>
public class WebModule : Autofac.Module
{
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx => ctx.Resolve<IAppContextAccessor>().CurrentAppContext).As<IAppContext>().InstancePerLifetimeScope();

        builder.RegisterType<AppContextAccessor>().As<IAppContextAccessor>().InstancePerLifetimeScope();
    }
}
