namespace Keka.Notifications.DB.Migrator;

public static class Program
{
    public static int Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json");

        var config = configuration.Build();

        if (bool.Parse(config["Vault:Enabled"]))
        {
            var options = new DefaultAzureCredentialOptions
            {
                TenantId = config["Vault:TenantId"],
            };

            var vaultConfiguration = new ConfigurationBuilder()
                .AddAzureKeyVault(new Uri(config["Vault:VaultUri"]), new DefaultAzureCredential(options))
                .Build();

            configuration.AddConfiguration(vaultConfiguration);
            config = configuration.Build();
        }

        var connectionString = args.FirstOrDefault() ?? config.GetConnectionString("NotificationsDB");

        var upgrader = DeployChanges
            .To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }
}
