// -----------------------------------------------------------------------
// <copyright file="EmailLambdaAppInfraModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Registrations;

/// <summary>
/// Represents the email lambda app module registrations.
/// </summary>
public static class EmailLambdaAppInfraModule
{
    /// <summary>
    /// Registers the add email lambda app registrations.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// /// <param name="configuration">The configuration object.</param>
    /// <returns>The web application.</returns>
    public static IServiceCollection AddEmailLambdaAppInfraRegistrations(this IServiceCollection services, IConfiguration configuration)
    {
        // Repository registrations.
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IEmailRequestRepository, EmailRequestRepository>();
        services.AddScoped<IEmailAttachmentRepository, EmailAttachmentRepository>();
        services.AddScoped<IEmailDeliveryHistoryRepository, EmailDeliveryHistoryRepository>();
        services.AddScoped<IEmailStatusRepository, EmailStatusRepository>();

        // Mapper.
        services.AddAutoMapper(typeof(EmailLambdaAppMapperProfile).Assembly);

        // Email provider.
        services.AddSingleton(x =>
        {
            var accessKeyId = configuration.GetRequiredValue("AWSAccessKeyId");
            var secretAccessKey = configuration.GetRequiredValue("AWSSecretKey");
            var region = configuration.GetRequiredValue("AWSRegion");
            var configurationSetName = configuration.GetValue<string>("AWSConfigurationSet");
            var s3BucketName = configuration.GetValue<string>("AWSS3BucketName");
            var awsOptions = new AwsOptions(accessKeyId, secretAccessKey, region, configurationSetName, s3BucketName);
            return awsOptions;
        });
        services.AddScoped<IEmailProvider, AmazonSesEmailProvider>();

        return services;
    }
}
