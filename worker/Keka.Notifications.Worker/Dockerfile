# Use the Microsoft official .NET Core SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and sln files
COPY Services/Keka.Notifications.Worker/*.csproj ./Services/Keka.Notifications.Worker/
COPY Services/Keka.Notifications.Application/*.csproj ./Services/Keka.Notifications.Application/
COPY Services/Keka.Notifications.Domain/*.csproj ./Services/Keka.Notifications.Domain/
COPY Services/Keka.Notifications.Infrastructure/*.csproj ./Services/Keka.Notifications.Infrastructure/

# Copy the rest of the source code
COPY Services/Keka.Notifications.Worker/ ./Services/Keka.Notifications.Worker/
COPY Services/Keka.Notifications.Application/ ./Services/Keka.Notifications.Application/
COPY Services/Keka.Notifications.Domain/ ./Services/Keka.Notifications.Domain/
COPY Services/Keka.Notifications.Infrastructure/ ./Services/Keka.Notifications.Infrastructure/

# Publish the application to a directory
RUN dotnet publish Keka.Notifications.Worker -c Release -o out

# Use the runtime image to create a lighter container
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port the app runs on
EXPOSE 80
EXPOSE 443

# Start the application
ENTRYPOINT ["dotnet", "Keka.Notifications.Worker.dll"]
