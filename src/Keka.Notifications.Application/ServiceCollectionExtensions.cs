// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure;

/// <summary>
/// Common extensions for KekaAppBuilder.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds web api registrations to the ContainerBuilder.
    /// </summary>
    /// <param name="builder">The ContainerBuilder instance.</param>
    /// <returns>The updated ContainerBuilder instance.</returns>
    public static ContainerBuilder AddWebApiApplicationRegistrations(this ContainerBuilder builder)
    {
        // You can also add new module registrations here if needed.
        builder.RegisterModule(new ApiAppModule());
        builder.RegisterModule(new CommonAppModule());

        // You can also integrate AutoMapper configurations here if needed.
        builder.RegisterAutoMapper(typeof(ApiDtoMapperProfile).Assembly);
        builder.RegisterAutoMapper(typeof(CommonDtoMapperProfile).Assembly);
        return builder;
    }

    /// <summary>
    /// Adds Worker registrations to the KekaAppBuilder.
    /// </summary>
    /// <param name="builder">The ContainerBuilder instance.</param>
    /// <returns>The updated ContainerBuilder instance.</returns>
    public static ContainerBuilder AddWorkerApplicationRegistrations(this ContainerBuilder builder)
    {
        // You can also add new module registrations here if needed.
        builder.RegisterModule(new CommonAppModule());
        builder.RegisterModule(new WorkerAppModule());

        // You can also integrate AutoMapper configurations here if needed.
        builder.RegisterAutoMapper(typeof(WorkerDtoMapperProfile).Assembly);

        return builder;
    }

    /// <summary>
    /// Adds messaging function app registrations to the ContainerBuilder.
    /// </summary>
    /// <param name="builder">The ContainerBuilder instance.</param>
    /// <returns>The updated ContainerBuilder instance.</returns>
    public static ContainerBuilder AddMessagingFunctionAppApplicationRegistrations(this ContainerBuilder builder)
    {
        // You can also add new module registrations here if needed.
        builder.RegisterModule(new MessagingFunctionAppModule());

        // You can also integrate AutoMapper configurations here if needed.
        builder.RegisterAutoMapper(typeof(MessagingFunctionAppDtoMapperProfile).Assembly);

        return builder;
    }

    /// <summary>
    /// Adds email function app registrations to the KekaAppBuilder.
    /// </summary>
    /// <param name="builder">The ContainerBuilder instance.</param>
    /// <returns>The updated ContainerBuilder instance.</returns>
    public static ContainerBuilder AddEmailFunctionAppApplicationRegistrations(this ContainerBuilder builder)
    {
        // You can also add new module registrations here if needed.
        builder.RegisterModule(new EmailFunctionAppModule());

        // You can also integrate AutoMapper configurations here if needed.
        builder.RegisterAutoMapper(typeof(EmailFunctionAppDtoMapperProfile).Assembly);

        return builder;
    }

    /// <summary>
    /// Adds email lambda app registrations to service collection.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddEmailLambdaAppApplicationRegistrations(this IServiceCollection services)
    {
        // You can also add new module registrations here if needed.
        services.AddScoped<IEmailDeliveryService, EmailDeliveryService>();
        services.AddScoped<IEmailDeliveryConverter, EmailDeliveryConverter>();

        // You can also integrate AutoMapper configurations here if needed.
        services.AddAutoMapper(typeof(EmailLambdaAppDtoMapperProfile).Assembly);

        return services;
    }
}
