// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.WebAPI;

/// <summary>
/// Represents the application entry.
/// </summary>
public static class Program
{
    /// <summary>
    /// Application entry.
    /// </summary>
    /// <param name="args">Any params.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddKeyVault();
        builder.Host.UseLogging();

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            // Register web api related registrations and automappers.
            containerBuilder.AddWebApiInfraRegistrations(builder.Configuration);
            containerBuilder.AddWebApiApplicationRegistrations();

            // Register Web modules
            containerBuilder.RegisterModule(new WebModule());

            containerBuilder.RegisterType<AllowOnlyIfEnabledAttribute>().As<IAuthorizationFilter>();
        });

        builder.Services
            .ConfigureThreadPoolSettings(builder.Configuration)
            .AddKekaApp(configuration: builder.Configuration)
            .AddWebApi()
            .AddHealthChecks(builder.Configuration)
            .AddWebApiInfrastructure()
            .Build();

        var app = builder.Build();

        app.MapDefaultHealthChecks();

        app.UseWebApiInfrastructure()
      .UseEndpoints(e => e.MapControllers())
      .UseAlwaysOn()
      .Build();

        app.Run();
    }
}