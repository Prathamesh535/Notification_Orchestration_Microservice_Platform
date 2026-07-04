using Keka.Application;

namespace Keka.Notifications.Client;

public static class Extensions
{
    public static IKekaAppBuilder AddNotificationService(this IKekaAppBuilder builder)
    {
        builder.Services.AddTransient<INotificationService, NotificationService>();
        return builder;
    }
}
