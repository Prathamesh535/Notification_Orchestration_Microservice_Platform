// -----------------------------------------------------------------------
// <copyright file="IEmailAttachmentRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents an email attachment repository.
/// </summary>
public interface IEmailAttachmentRepository
{
    /// <summary>
    /// Get Email Attachment.
    /// </summary>
    /// <param name="emailAttachmentId">The email Attachment Id.</param>
    /// <returns>Task Stream.</returns>
    Task<Stream> GetEmailAttachmentAsync(string emailAttachmentId);

    /// <summary>
    /// Save Email Attachments.
    /// </summary>
    /// <param name="emailAttachmentStream">The email attachment stream.</param>
    /// <param name="contentType">The content Type.</param>
    /// <returns>Email Attachment Id.</returns>
    Task<string> SaveEmailAttachmentAsync(Stream emailAttachmentStream, string contentType);

    /// <summary>
    /// Deletes the email record.
    /// </summary>
    /// <param name="attachmentIds">The ID of the attachments to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteEmailAttachmentAsync(List<string> attachmentIds);
}