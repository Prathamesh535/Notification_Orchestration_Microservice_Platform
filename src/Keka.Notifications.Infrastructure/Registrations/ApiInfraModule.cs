// -----------------------------------------------------------------------
// <copyright file="ApiInfraModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Registrations;

/// <summary>
/// Represents the api infra autofac module.
/// </summary>
/// <seealso cref="Module" />
public class ApiInfraModule()
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
        builder.RegisterType<JobConfigRepository>().As<IJobConfigRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
        RegisterEmailRepositories(builder);
        RegisterMessagingRepositories(builder);
    }

    private static void RegisterEmailRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<EmailRepository>().As<IEmailRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailRequestRepository>().As<IEmailRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailAttachmentRepository>().As<IEmailAttachmentRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailTemplateRepository>().As<IEmailTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailDeliveryHistoryRepository>().As<IEmailDeliveryHistoryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailStatusRepository>().As<IEmailStatusRepository>().InstancePerLifetimeScope();
        builder.Register(ctx =>
        {
            var config = ctx.Resolve<IConfiguration>();
            var accessKeyId = config.GetRequiredValue("AWSAccessKeyId");
            var secretAccessKey = config.GetRequiredValue("AWSSecretKey");
            var region = config.GetRequiredValue("AWSRegion");
            var configurationSetName = config.GetValue<string>("AWSConfigurationSet");
            var s3BucketName = config.GetValue<string>("AWSS3BucketName");
            var awsOptions = new AwsOptions(accessKeyId, secretAccessKey, region, configurationSetName, s3BucketName);
            return awsOptions;
        }).As<AwsOptions>().SingleInstance();

        builder.RegisterType<AmazonSesEmailProvider>()
            .As<IEmailProvider>()
            .InstancePerLifetimeScope();
    }

    private static void RegisterMessagingRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<InAppNotificationRequestRepository>().As<IInAppNotificationRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<InAppNotificationTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedInAppNotificationTemplateRepository<InAppNotificationTemplateRepository>>().As<IInAppNotificationTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<InAppNotificationRepository>().As<IInAppNotificationRepository>().InstancePerLifetimeScope();
        builder.RegisterType<WaMessageRepository>().As<IWaMessageRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SmsRequestRepository>().As<ISmsRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmployeeNotificationPreferenceRepository>().As<IEmployeeNotificationPreferenceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PushNotificationRequestRepository>().As<IPushNotificationRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SmsTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedSmsTemplateRepository<SmsTemplateRepository>>().As<ISmsTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PushNotificationTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedPushNotificationTemplateRepository<PushNotificationTemplateRepository>>().As<IPushNotificationTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CountryRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedCountryRepository<CountryRepository>>().As<ICountryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<WaTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedWaTemplateRepository<WaTemplateRepository>>().As<IWaTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<WebhookRequestRepository>().As<IWebhookRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SlackRequestRepository>().As<ISlackRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SlackNotificationTemplateRepository>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<CachedSlackNotificationTemplateRepository<SlackNotificationTemplateRepository>>().As<ISlackNotificationTemplateRepository>().InstancePerLifetimeScope();
        builder.Register(ctx =>
        {
            var config = ctx.Resolve<IConfiguration>();
            var accessKeyId = config.GetRequiredValue("AWSAccessKeyId");
            var secretAccessKey = config.GetRequiredValue("AWSSecretKey");
            var region = config.GetRequiredValue("AWSRegion");
            var configurationSetName = config.GetValue<string>("AWSConfigurationSet");
            var s3BucketName = config.GetValue<string>("AWSS3BucketName");
            var awsOptions = new AwsOptions(accessKeyId, secretAccessKey, region, configurationSetName, s3BucketName);
            return awsOptions;
        }).As<AwsOptions>().SingleInstance();

        builder.RegisterType<AmazonSesEmailProvider>()
            .As<IEmailProvider>()
            .InstancePerLifetimeScope();
    }
}
