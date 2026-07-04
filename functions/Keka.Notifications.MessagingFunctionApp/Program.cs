namespace Keka.Notifications.MessagingFunctionApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWebApplication()
            .ConfigureAppConfiguration((context, config) =>
            {
                var builderConfig = config.Build();

                var options = new DefaultAzureCredentialOptions
                {
                    TenantId = builderConfig.GetRequiredValue("VaultTenantId"),
                };

                config.AddAzureKeyVault(new Uri(builderConfig.GetRequiredValue("VaultUri")), new DefaultAzureCredential(options));
            })
            .ConfigureServices((builderContext, services) =>
            {
                services
                 .AddLogging()
                 .AddKekaApp(configuration: builderContext.Configuration)
                 .AddOneSignal()
                 .AddMessagingFunctionAppInfrastructure();
            })
            .ConfigureAutofac()
            .UseLogging()
            .Build();

        await host.RunAsync();
    }

    private static IHostBuilder ConfigureAutofac(this IHostBuilder hostbuilder)
    {
        hostbuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>((hostContext, builder) =>
            {
                var configuration = hostContext.Configuration;

                // Register messaging function app related registrations and automappers.
                builder.AddMessagingFunctionAppInfraRegistrations(configuration);
                builder.AddMessagingFunctionAppApplicationRegistrations();

                // Register messaging services
                RegisterMessagingServices(configuration, builder);

                // Register Push Notification configurations
                RegisterPushNotificationServices(hostContext, builder);

                // Register Slack Services
                RegisterSlackNotificationServices(builder);
            });

        return hostbuilder;
    }

    private static void RegisterMessagingServices(IConfiguration context, ContainerBuilder builder)
    {
        var msg91Options = new Msg91Options()
        {
            AuthKey = context.GetRequiredValue("Msg91:AuthKey"),
            BaseUrl = context.GetRequiredValue("Msg91:BaseUrl")
        };

        // Register WhatsApp services
        builder.Register(ctx => new Msg91WaMessageSender(msg91Options, ctx.Resolve<IHttpClient>()))
               .As<IWaMessageSender>()
               .InstancePerLifetimeScope();

        // Register SMS services
        builder.Register(ctx => new Msg91SmsSender(msg91Options, ctx.Resolve<IHttpClient>()))
               .As<ISmsSender>()
               .InstancePerLifetimeScope();
    }

    private static void RegisterPushNotificationServices(HostBuilderContext context, ContainerBuilder builder)
    {
        builder.Register(ctx =>
        {
            var projectId = context.Configuration.GetRequiredValue("Firebase:ProjectId");
            var serviceAccountJson = context.Configuration.GetRequiredValue("Firebase:ServiceAccountJson");
            return new FcmNotificationSender(new FcmOptions { ProjectId = projectId, ServiceAccountJson = serviceAccountJson });
        }).As<IPushNotificationSender>()
                 .SingleInstance();
    }

    private static void RegisterSlackNotificationServices(ContainerBuilder builder)
    {
        // Register Slack notification services
        builder.Register(ctx => new SlackProvider(ctx.Resolve<IHttpClient>()))
               .As<ISlackProvider>()
               .InstancePerLifetimeScope();
    }
}