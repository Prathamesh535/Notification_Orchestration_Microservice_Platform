// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker;

/// <summary>
/// Represents the application entry.
/// </summary>
public static class Program
{
    /// <summary>
    /// Application entry.
    /// </summary>
    /// <param name="args">The arguments to be passed to application entry.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddKeyVault();
        builder.Host.UseLogging();

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            // Register Autofac modules
            containerBuilder.RegisterModule(new WorkerModule());

            // Register worker related registrations and automappers.
            containerBuilder.AddWorkerInfraRegistrations(builder.Configuration);
            containerBuilder.AddWorkerApplicationRegistrations();
        });

        builder.Services
            .ConfigureThreadPoolSettings(builder.Configuration)
            .AddKekaApp(configuration: builder.Configuration)
            .AddHealthChecks(builder.Configuration)
            .AddWorkerInfrastructure()
            .AddHangfire()
            .Build();

        var app = builder.Build();

        app.MapDefaultHealthChecks();

        app.UseAlwaysOn()
            .UseWorkerInfrastructure()
            .UseHangfire();

        app.RegisterEvents();

        //// Here we are scheduling the job daily based on configuration.
        RecurringJob.AddOrUpdate("Schedule Jobs", () => app.Services.GetRequiredService<JobScheduler>().ScheduleJobs(), Cron.Daily());

        app.Run();
    }
}