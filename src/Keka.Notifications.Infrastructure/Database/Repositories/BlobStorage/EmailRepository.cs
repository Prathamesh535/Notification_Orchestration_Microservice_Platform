// -----------------------------------------------------------------------
// <copyright file="EmailRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.BlobStorage;

internal class EmailRepository(IAppContext appContext, IBlobStorageService blobStorageService, IDateTimeService dateTimeService)
    : IEmailRepository
{
    private readonly string containerName = "emails";

    /// <inheritdoc/>
    public async Task<Email> GetEmailAsync(string emailId)
    {
        using (var emailStream = new StreamReader(await blobStorageService.DownloadAsync(this.containerName, emailId)))
        {
            var emailRequestJson = await emailStream.ReadToEndAsync();
            return emailRequestJson.FromJson<Email>();
        }
    }

    /// <inheritdoc/>
    public async Task<string> SaveEmailAsync(Email email)
    {
        var emailJson = email.ToJson();
        var currentDate = dateTimeService.GetCurrentTimeUtc();
        var emailId = $"{appContext.TenantId}/{currentDate.Year}/{currentDate.Month}/{currentDate.Day}/{Guid.NewGuid()}";

        using (var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(emailJson)))
        {
            await blobStorageService.UploadAsync(this.containerName, emailId, memoryStream, "application/json");
        }

        return emailId;
    }

    /// <inheritdoc/>
    public async Task DeleteEmailAsync(string emailId)
    {
        await blobStorageService.DeleteAsync(this.containerName, emailId);
    }
}
