// -----------------------------------------------------------------------
// <copyright file="EmailAttachmentRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories.BlobStorage;

/// <summary>
/// Represents the in Email Attachment repository.
/// </summary>
/// <param name="appContext">The app context.</param>
/// <param name="blobStorageService">The Blob Storage service.</param>
public class EmailAttachmentRepository(IAppContext appContext, IBlobStorageService blobStorageService)
    : IEmailAttachmentRepository
{
    private readonly string containerName = "email-attachments";

    /// <inheritdoc/>
    public async Task<Stream> GetEmailAttachmentAsync(string emailAttachmentId)
    {
        return await blobStorageService.DownloadAsync(this.containerName, emailAttachmentId);
    }

    /// <inheritdoc/>
    public async Task<string> SaveEmailAttachmentAsync(Stream emailAttachmentStream, string contentType)
    {
        var emailAttachmentId = $"{appContext.TenantId}/{Guid.NewGuid()}";
        await blobStorageService.UploadAsync(this.containerName, emailAttachmentId, emailAttachmentStream, contentType);
        return emailAttachmentId;
    }

    /// <inheritdoc/>
    public async Task DeleteEmailAttachmentAsync(List<string> attachmentIds)
    {
        await Task.WhenAll(attachmentIds.Select(attachmentId => blobStorageService.DeleteAsync(this.containerName, attachmentId)).ToArray());
    }
}