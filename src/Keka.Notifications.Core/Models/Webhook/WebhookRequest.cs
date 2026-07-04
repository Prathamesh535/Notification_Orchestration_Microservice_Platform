// -----------------------------------------------------------------------
// <copyright file="WebhookRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models.Webhook;

/// <summary>
/// Represents webhook request class.
/// </summary>
public class WebhookRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebhookRequest"/> class.
    /// </summary>
    public WebhookRequest()
    {
        this.RequestHeaders = new Dictionary<string, string>();
    }

    /// <summary>
    /// Gets or sets the webhook request identifier.
    /// </summary>
    public Guid WebhookRequestId { get; set; }

    /// <summary>
    /// Gets or sets the endpoint of the webhook request.
    /// </summary>
    public string EndPoint { get; set; }

    /// <summary>
    /// Gets or sets the status of the webhook request.
    /// </summary>
    public NotificationStatus NotificationStatus { get; set; }

    /// <summary>
    /// Gets or sets the request headers of the webhook request.
    /// </summary>
    public Dictionary<string, string> RequestHeaders { get; set; }

    /// <summary>
    /// Gets or sets the payload of the webhook request.
    /// </summary>
    public object Payload { get; set; }

    /// <summary>
    /// Gets or sets the response of the webhook request.
    /// </summary>
    public string RawResponse { get; set; }

    /// <summary>
    /// Gets or sets the HTTP method.
    /// </summary>
    public HttpMethodType HttpMethod { get; set; }
}
