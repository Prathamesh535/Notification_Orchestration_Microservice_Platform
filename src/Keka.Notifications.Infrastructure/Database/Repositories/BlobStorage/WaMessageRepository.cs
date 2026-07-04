// -----------------------------------------------------------------------
// <copyright file="WaMessageRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.BlobStorage;

/// <summary>
/// Represents the whatsapp message repository.
/// </summary>
/// <param name="dateTimeService">The dateTime Service.</param>
/// <param name="blobStorageService">The blobStorage Service.</param>
/// <param name="appContext">The app context.</param>
internal class WaMessageRepository(IAppContext appContext, IBlobStorageService blobStorageService, IDateTimeService dateTimeService)
    : IWaMessageRepository
{
    private readonly string containerName = "whatsapp-messages";

    /// <inheritdoc/>
    public async Task<string> SaveWaMessageAsync(WaMessage waMessage)
    {
        var waMessgeJson = waMessage.ToJson();
        var currentDate = dateTimeService.GetCurrentTimeUtc();
        var waMessageId = $"{appContext.TenantId}/{currentDate.Year}/{currentDate.Month}/{currentDate.Day}/{waMessage.WaMessageId}";

        using (var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(waMessgeJson)))
        {
            await blobStorageService.UploadAsync(this.containerName, waMessageId, memoryStream, "application/json");
        }

        return waMessageId;
    }

    /// <inheritdoc/>
    public async Task<WaMessage> GetWaMessageAsync(string waMessageId)
    {
        using (var stream = new StreamReader(await blobStorageService.DownloadAsync(this.containerName, waMessageId)))
        {
            var waMessgeJson = await stream.ReadToEndAsync();
            return waMessgeJson.FromJson<WaMessage>();
        }
    }

    /// <inheritdoc/>
    public async Task UpdateWaMessageAsync(string waMessageId, WaMessage waMessage)
    {
        var waMessgeJson = waMessage.ToJson();
        using (var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(waMessgeJson)))
        {
            await blobStorageService.UploadAsync(this.containerName, waMessageId, memoryStream, "application/json");
        }
    }
}