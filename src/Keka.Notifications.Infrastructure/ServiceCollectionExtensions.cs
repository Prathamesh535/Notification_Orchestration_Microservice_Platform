// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Keka.Persistence.Blobs;

namespace Keka.Notifications.Infrastructure;

/// <summary>
/// Common extensions for KekaAppBuilder.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds web api infrastructure services to the KekaAppBuilder.
    /// </summary>
    /// <param name="builder">The KekaAppBuilder instance.</param>
    /// <returns>The updated KekaAppBuilder instance.</returns>
    public static IKekaAppBuilder AddWebApiInfrastructure(this IKekaAppBuilder builder)
    {
        builder
            .AddErrorHandler<ExceptionToResponseMapper>()
            .AddJwt()
            .AddKekaOAuth2Client()
            .AddHttpClient("Notifications", httpClientBuilder: clientBuilder =>
            {
                clientBuilder.AddHttpMessageHandler<KekaAuthTokenHandler>();
            })
            .AddRedis()
            .AddDistributedAccessTokenValidator()
            .AddPrometheus()
            .AddApplicationInsights()
            .AddWebApiSwaggerDocs()
            .AddServiceBus()
            .AddSecurity()
            .AddSupportOps()
            .AddTableStorage()
            .AddBlobStorage();

        return builder;
    }

    /// <summary>
    /// Adds web api registrations to the ContainerBuilder.
    /// </summary>
    /// <param name="builder">The ContainerBuilder instance.</param>
    /// <param name="configuration">The Configuration instance.</param>
    /// <returns>The updated ContainerBuilder instance.</returns>
    public static ContainerBuilder AddWebApiInfraRegistrations(this ContainerBuilder builder, IConfiguration configuration)
    {
        // You can also add new module registrations here if needed.
        builder.RegisterModule(new ApiInfraModule());
        builder.RegisterModule(new CommonInfraModule(configuration));

        // You can also integrate AutoMapper configurations here if needed.
        builder.RegisterAutoMapper(typeof(ApiMapperProfile).Assembly);
        builder.RegisterAutoMapper(typeof(CommonMapperProfile).Assembly);

        return builder;
    }

    /// <summary>
    /// Configures web api infrastructure for the application.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance.</param>
    /// <returns>The updated IApplicationBuilder instance.</returns>
    public static IApplicationBuilder UseWebApiInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandler()
            .UseKekaApp()
            .UseSwaggerDocs()
            .UseAccessTokenValidator()
            .UsePrometheus()
            .UseRouting()
            .UseAuthorization()
            .UseSupportOps()
            .UseMiddleware<AppContextMiddleware>();

        return app;
    }

    /// <summary>
    /// Adds worker infrastructure services to the KekaAppBuilder.
    /// </summary>
    /// <param name="builder">The KekaAppBuilder instance.</param>
    /// <returns>The updated KekaAppBuilder instance.</returns>
    public static IKekaAppBuilder AddWorkerInfrastructure(this IKekaAppBuilder builder)
    {
        builder.AddErrorHandler<ExceptionToResponseMapper>()
            .AddKekaOAuth2Client()
            .AddHttpClient("Notifications", httpClientBuilder: clientBuilder =>
            {
                clientBuilder.AddHttpMessageHandler<KekaAuthTokenHandler>();
            })
            .AddRedis()
            .AddPrometheus()
            .AddApplicationInsights()
            .AddServiceBus()
            .AddSecurity()
            .AddTableStorage()
            .AddBlobStorage();

        return builder;
    }

    /// <summary>
    /// Adds Worker registrations to the KekaAppBuilder.
    /// </summary>
    /// <param name="builder">The ContainerBuilder instance.</param>
    /// <param name="configuration">The Configuration instance.</param>
    /// <returns>The updated ContainerBuilder instance.</returns>
    public static ContainerBuilder AddWorkerInfraRegistrations(this ContainerBuilder builder, IConfiguration configuration)
    {
        // You can also add new module registrations here if needed.
        builder.RegisterModule(new CommonInfraModule(configuration));
        builder.RegisterModule(new WorkerInfraModule());

        // You can also integrate AutoMapper configurations here if needed.
        builder.RegisterAutoMapper(typeof(WorkerMapperProfile).Assembly);

        return builder;
    }

    /// <summary>
    /// Configures worker infrastructure for the application.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance.</param>
    /// <returns>The updated IApplicationBuilder instance.</returns>
    public static IApplicationBuilder UseWorkerInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandler()
            .UseKekaApp()
            .UsePrometheus();

        app.UseMiddleware<AppContextMiddleware>();

        return app;
    }

    /// <summary>
    /// Adds function app infrastructure services to the KekaAppBuilder.
    /// </summary>
    /// <param name="builder">The KekaAppBuilder instance.</param>
    /// <returns>The updated KekaAppBuilder instance.</returns>
    public static IKekaAppBuilder AddMessagingFunctionAppInfrastructure(this IKekaAppBuilder builder)
    {
        builder.AddApplicationInsights();
        builder.AddInfraServiceBus();
        builder.AddInfraStorage();
        builder.AddInfraRedisCache();
        builder.Services.AddScoped<ISmsRequestRepository, SmsRequestRepository>();
        builder.Services.AddScoped<IWaMessageRepository, WaMessageRepository>();
        builder.Services.AddScoped<IWebhookRequestRepository, WebhookRequestRepository>();
        return builder;
    }

    /// <summary>
    /// Adds messaging function app registrations to the ContainerBuilder.
    /// </summary>
    /// <param name="builder">The ContainerBuilder instance.</param>
    /// <param name="configuration">The Configuration instance.</param>
    /// <returns>The updated ContainerBuilder instance.</returns>
    public static ContainerBuilder AddMessagingFunctionAppInfraRegistrations(this ContainerBuilder builder, IConfiguration configuration)
    {
        // You can also add new module registrations here if needed.
        builder.RegisterModule(new MessagingFunctionAppInfraModule());
        builder.RegisterModule(new CommonInfraModule(configuration));

        // You can also integrate AutoMapper configurations here if needed.
        builder.RegisterAutoMapper(typeof(MessagingFunctionAppMapperProfile).Assembly);

        return builder;
    }

    /// <summary>
    /// Adds function app infrastructure services to the KekaAppBuilder.
    /// </summary>
    /// <param name="builder">The KekaAppBuilder instance.</param>
    /// <returns>The updated KekaAppBuilder instance.</returns>
    public static IKekaAppBuilder AddEmailFunctionAppInfrastructure(this IKekaAppBuilder builder)
    {
        builder
            .AddApplicationInsights()
            .AddInfraServiceBus()
            .AddInfraStorage();

        return builder;
    }

    /// <summary>
    /// Adds email function app registrations to the KekaAppBuilder.
    /// </summary>
    /// <param name="builder">The ContainerBuilder instance.</param>
    /// <param name="configuration">Configuration object.</param>
    /// <returns>The updated ContainerBuilder instance.</returns>
    public static ContainerBuilder AddEmailFunctionAppInfraRegistrations(this ContainerBuilder builder, IConfiguration configuration)
    {
        // You can also add new module registrations here if needed.
        builder.RegisterModule(new EmailFunctionAppInfraModule());
        builder.RegisterModule(new CommonInfraModule(configuration));

        // You can also integrate AutoMapper configurations here if needed.
        builder.RegisterAutoMapper(typeof(EmailFunctionAppMapperProfile).Assembly);

        return builder;
    }

    /// <summary>
    /// Adds email lambda app registrations to service collection.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration object.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddEmailLambdaAppRegistrations(this IServiceCollection services, IConfiguration configuration)
    {
        // You can also add new module registrations here if needed.
        services
            .AddKekaApp(configuration: configuration)
            .AddInfraStorage();
        services.AddScoped(provider =>
        {
            var accessor = provider.GetRequiredService<IAppContextAccessor>();
            return accessor.CurrentAppContext;
        });

        services.AddScoped(provider =>
        {
            var appContext = provider.GetRequiredService<IAppContext>();
            var connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("NotificationsDB");
            return new DatabaseContext(connectionString, appContext);
        });

        services.AddScoped<IAppContextAccessor, AppContextAccessor>();
        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddEmailLambdaAppInfraRegistrations(configuration);

        return services;
    }

    /// <summary>
    /// Registers the events.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <returns>The web application.</returns>
    public static WebApplication RegisterEvents(this WebApplication app)
    {
        var eventBus = app.Services.GetRequiredService<IEventBus>();
        eventBus.SubscribeTenantEvent<EmailRequestReceivedEvent, IEventHandler<EmailRequestReceivedEvent>>();
        eventBus.SubscribeTenantEvent<InAppNotificationRequestReceivedEvent, IEventHandler<InAppNotificationRequestReceivedEvent>>();
        eventBus.SubscribeTenantEvent<EmployeeAddedEvent, IEventHandler<EmployeeAddedEvent>>();
        eventBus.SubscribeTenantEvent<EmployeeEmailUpdatedEvent, IEventHandler<EmployeeEmailUpdatedEvent>>();
        return app;
    }

    /// <summary>
    /// Configure Thread Pool Settings.
    /// </summary>
    /// <param name="services">The Services.</param>
    /// <param name="configuration">The builder configuration.</param>
    /// <returns>Service Collection.</returns>
    public static IServiceCollection ConfigureThreadPoolSettings(this IServiceCollection services, IConfiguration configuration)
    {
        // IMPORTANT:- By default App Service.
        // Windows Server threads are defaulted to 50 per core. Redis is Thread intensive..
        // Without increasing default thread count, application will crash if it gets more than 25 concurrent requests per second.
        // As it will increase the latency and timeout errors will come.
        var minWorkerThreads = configuration.GetValue<int>("MinWorkerThreads");
        var minIoThreads = configuration.GetValue<int>("MinIOThreads");
        ServicePointManager.DefaultConnectionLimit = minWorkerThreads;
        ThreadPool.GetMaxThreads(out int _, out int completionThreads);
        ThreadPool.SetMinThreads(minWorkerThreads, Math.Max(minIoThreads, completionThreads));
        return services;
    }

    /// <summary>
    /// Adds the azure key vault.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>The keka app builder.</returns>
    public static WebApplicationBuilder AddKeyVault(this WebApplicationBuilder builder)
    {
        if (!bool.Parse(builder.Configuration.GetRequiredValue("Vault:Enabled")))
        {
            return builder;
        }

        var options = new DefaultAzureCredentialOptions
        {
            TenantId = builder.Configuration["Vault:TenantId"],
        };

        builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration.GetRequiredValue("Vault:VaultUri")), new DefaultAzureCredential(options));

        return builder;
    }

    private static IKekaAppBuilder AddInfraServiceBus(this IKekaAppBuilder builder)
    {
        builder.AddServiceBus(new ServiceBusOptions()
        {
            Enabled = true,
            ConnectionStringName = "EventBus",
            TopicName = "keka_event_bus",
        });

        return builder;
    }

    private static IKekaAppBuilder AddInfraStorage(this IKekaAppBuilder builder)
    {
        builder.AddTableStorage(new TableStorageOptions()
        {
            Enabled = true,
            ConnectionStringName = "NotificationsTableStorage",
        });

        builder.AddBlobStorage(new BlobStorageOptions()
        {
            Enabled = true,
            ConnectionStringName = "NotificationsTableStorage",
        });

        return builder;
    }

    private static IKekaAppBuilder AddInfraRedisCache(this IKekaAppBuilder builder)
    {
        builder.AddRedis(new RedisOptions()
        {
            ConnectionStringName = "Redis",
            Instance = "ns:",
        });

        return builder;
    }
}