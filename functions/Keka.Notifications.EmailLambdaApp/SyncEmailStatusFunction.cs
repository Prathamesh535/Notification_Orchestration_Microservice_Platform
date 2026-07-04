// -----------------------------------------------------------------------
// <copyright file="SyncEmailStatusFunction.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Keka.Notifications.EmailLambdaApp;

/// <summary>
/// Represents an AWS Lambda function handler for processing SQS events and storing email delivery status details in Azure Table Storage.
/// </summary>
public class SyncEmailStatusFunction
{
    private readonly ServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SyncEmailStatusFunction"/> class.
    /// </summary>
    public SyncEmailStatusFunction()
    {
        var services = new ServiceCollection();
        this.serviceProvider = ConfigureServices(services);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="SyncEmailStatusFunction"/> class.
    /// </summary>
    ~SyncEmailStatusFunction()
    {
        if (this.serviceProvider is not null)
        {
            this.serviceProvider.Dispose();
        }
    }

    /// <summary>
    /// AWS Lambda function handler to process SQS events.
    /// </summary>
    /// <param name="sqsEvent">The SQS event.</param>
    /// <param name="lambdaContext">The lambda context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task FunctionHandler(SQSEvent sqsEvent, ILambdaContext lambdaContext)
    {
        // Obtain required services
        using (var scope = this.serviceProvider.CreateScope())
        {
            var sqsEventConverter = scope.ServiceProvider.GetService<ISqsEventConverter>();
            var emailDeliveryService = scope.ServiceProvider.GetService<IEmailDeliveryService>();
            if (sqsEventConverter is null || emailDeliveryService is null)
            {
                throw new InvalidOperationException("Unable to obtain dependencies.");
            }

            // Convert to dtos
            var sqsEventDtos = sqsEventConverter!.ConvertToSqsEventDtos(sqsEvent);
            if (sqsEventDtos is null || sqsEventDtos.Count == 0)
            {
                lambdaContext.Logger.LogError("Invalid SQS event or empty records.");
            }

            // Sync events
            await emailDeliveryService!.SyncEmailDeliveryEventsAsync(sqsEventDtos);
        }
    }

    private static ServiceProvider ConfigureServices(IServiceCollection services)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .Build();

        // Register email lambda app related registrations and automappers.
        services.AddEmailLambdaAppRegistrations(configuration);
        services.AddEmailLambdaAppApplicationRegistrations();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<ISqsEventConverter, SqsEventConverter>();
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}