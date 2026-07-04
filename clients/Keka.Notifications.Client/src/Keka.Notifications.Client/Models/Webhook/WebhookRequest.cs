// -----------------------------------------------------------------------
// <copyright file="WebhookRequest.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Client.Models.Webhook;

/// <summary>
/// Represents a webhook request class.
/// </summary>
public class WebhookRequest
{ 
    /// <summary>
    /// Gets or sets the endpoint of the webhook request.
    /// </summary>
    public string EndPoint { get; set; }

    /// <summary>
    /// Gets or sets the request headers of webhook request.
    /// </summary>
    public Dictionary<string, string> RequestHeaders { get; set; }

    /// <summary>
    /// Gets or sets the payload of webhook request.
    /// </summary>
    public object Payload { get; set; }

    /// <summary>
    /// Gets or sets the HTTP method.
    /// </summary>
    public string HttpMethod { get; set; }
}
