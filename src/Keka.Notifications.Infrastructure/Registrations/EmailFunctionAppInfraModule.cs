// -----------------------------------------------------------------------
// <copyright file="EmailFunctionAppInfraModule.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Registrations;

/// <summary>
/// Represents the email function app infrastructure autofac module.
/// </summary>
/// <seealso cref="Module" />
public class EmailFunctionAppInfraModule()
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
        builder.RegisterType<EmailRepository>().As<IEmailRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailRequestRepository>().As<IEmailRequestRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailAttachmentRepository>().As<IEmailAttachmentRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailTemplateRepository>().As<IEmailTemplateRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailDeliveryHistoryRepository>().As<IEmailDeliveryHistoryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmailStatusRepository>().As<IEmailStatusRepository>().InstancePerLifetimeScope();
        builder.Register(ctx => ctx.Resolve<IAppContextAccessor>().CurrentAppContext).As<IAppContext>()
                    .InstancePerLifetimeScope();
        builder.RegisterType<AppContextAccessor>().As<IAppContextAccessor>().InstancePerLifetimeScope();
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
