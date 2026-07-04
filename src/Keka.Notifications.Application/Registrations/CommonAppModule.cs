// -----------------------------------------------------------------------
// <copyright file="CommonAppModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application;

/// <summary>
/// Represents the common application related autofac module.
/// </summary>
public class CommonAppModule : Autofac.Module
{
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TemplateHelper>().As<ITemplateHelper>().SingleInstance();
    }
}