// -----------------------------------------------------------------------
// <copyright file="WaMessageRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Wa;

/// <summary>
/// Represents whatsapp message class.
/// </summary>
public class WaMessageRequest
{
    /// <summary>
    /// Gets or sets the wa message request ID.
    /// </summary>
    public Guid WaMessageRequestId { get; set; }

    /// <summary>
    /// Gets or sets template identifier.
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the list of personalization's for the wa message.
    /// </summary>
    public List<WaPersonalization> Personalization { get; set; }

    /// <summary>
    /// Gets or sets notification send status.
    /// </summary>
    public NotificationStatus Status { get; set; }

    /// <summary>
    /// Gets or sets external id.
    /// </summary>
    #nullable enable
    public string? ExternalId { get; set; }

    /// <summary>
    /// Gets or sets failure reason.
    /// </summary>
    public string? FailureReason { get; set; }
    #nullable restore
}
