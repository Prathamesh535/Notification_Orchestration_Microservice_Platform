// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Worker;

/// <summary>
/// ServiceCollectionExtensions for health check.
/// </summary>
public static class ServiceCollectionExtensions
{
    private static readonly string[] SelfTags = new string[] { "live" };
    private static readonly string[] SqlTags = new string[] { "sql", "live" };
    private static readonly string[] ServiceBusTags = new string[] { "servicebus", "live" };

    /// <summary>
    /// Get the application name.
    /// </summary>
    /// <param name="httpContext">The http context.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static Task GetAppName(this HttpContext httpContext)
        => httpContext.Response.WriteAsync(httpContext.RequestServices.GetService<AppOptions>().Name);

    /// <summary>
    /// Add health checks.
    /// </summary>
    /// <param name="builder">The Keka Application builder.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>Return the Keka application builder.</returns>
    public static IKekaAppBuilder AddHealthChecks(this IKekaAppBuilder builder, IConfiguration configuration)
    {
        var hcBuilder = builder.Services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy(), SelfTags);

        hcBuilder.AddSqlServer(configuration.GetRequiredConnectionString("FileHandlerDB"), name: "FileHandlerDB-check", tags: SqlTags);

        // Add health check for service bus
        hcBuilder.AddAzureServiceBusTopic(
            configuration.GetRequiredValue("ConnectionStrings:EventBus"),
            configuration.GetRequiredValue("ServiceBus:TopicName"),
            name: "FileHandlerServiceBus-check",
            tags: ServiceBusTags);

        hcBuilder.ForwardToPrometheus();

        return builder;
    }

    /// <summary>
    /// Map default health checks.
    /// </summary>
    /// <param name="app">The web application builder.</param>
    /// <returns>Return the Web Application builder.</returns>
    public static WebApplication MapDefaultHealthChecks(this WebApplication app)
    {
        // Add health check UI response writer.
        app.MapHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        // All health checks must pass for app to be considered ready to accept traffic after starting
        app.MapHealthChecks("/health");

        // Only health checks tagged with the "live" tag must pass for app to be considered alive
        app.MapHealthChecks("/liveness", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live"),
        });

        return app;
    }
}