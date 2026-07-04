namespace Keka.Notifications.EmailFunctionApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWebApplication().ConfigureAppConfiguration((context, config) =>
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
                services.AddLogging();
                services
                .AddKekaApp(configuration: builderContext.Configuration)
                .AddEmailFunctionAppInfrastructure();
            })
            .ConfigureAutofac()
            .UseLogging()
            .Build();
        await host.RunAsync();
    }

    private static IHostBuilder ConfigureAutofac(this IHostBuilder hostbuilder)
    {
        hostbuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>((context, builder) =>
            {
                // Register email function app related registration and automappers.
                builder.AddEmailFunctionAppInfraRegistrations(context.Configuration);
                builder.AddEmailFunctionAppApplicationRegistrations();
            });

        return hostbuilder;
    }
}