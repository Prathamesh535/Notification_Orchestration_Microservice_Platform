// -----------------------------------------------------------------------
// <copyright file="WebhookRequestDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Webhook;

/// <summary>
/// Represents Webhook request DTO.
/// </summary>
public class WebhookRequestDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebhookRequestDto"/> class.
    /// </summary>
    public WebhookRequestDto()
    {
        this.RequestHeaders = new Dictionary<string, string>();
    }

    /// <summary>
    /// Gets or sets the endpoint of the webhook request.
    /// </summary>
    [Required(ErrorMessage = "Endpoint is required field")]
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
