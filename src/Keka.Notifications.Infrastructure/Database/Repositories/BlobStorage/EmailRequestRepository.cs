// -----------------------------------------------------------------------
// <copyright file="EmailRequestRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.BlobStorage;

/// <summary>
/// Represents the email request repository.
/// </summary>
/// <param name="dateTimeService">The dateTime Service.</param>
/// <param name="blobStorageService">The blobStorage Service.</param>
/// <param name="appContext">The app context.</param>
internal class EmailRequestRepository(IAppContext appContext, IBlobStorageService blobStorageService, IDateTimeService dateTimeService)
    : IEmailRequestRepository
{
    private readonly string containerName = "email-requests";

    /// <inheritdoc/>
    public async Task<EmailRequest> GetEmailRequestAsync(string emailRequestId)
    {
        using (var emailRequestStream = new StreamReader(await blobStorageService.DownloadAsync(this.containerName, emailRequestId)))
        {
            var emailRequestJson = await emailRequestStream.ReadToEndAsync();
            return emailRequestJson.FromJson<EmailRequest>();
        }
    }

    /// <inheritdoc/>
    public async Task<string> SaveEmailRequestAsync(EmailRequest emailRequest)
    {
        var emailRequestJson = emailRequest.ToJson();
        var currentDate = dateTimeService.GetCurrentTimeUtc();
        var emailRequestId = $"{appContext.TenantId}/{currentDate.Year}/{currentDate.Month}/{currentDate.Day}/{emailRequest.EmailRequestId}";

        using (var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(emailRequestJson)))
        {
            await blobStorageService.UploadAsync(this.containerName, emailRequestId, memoryStream, "application/json");
        }

        return emailRequestId;
    }

    /// <inheritdoc/>
    public async Task DeleteEmailRequestAsync(string emailRequestId)
    {
        await blobStorageService.DeleteAsync(this.containerName, emailRequestId);
    }
}
