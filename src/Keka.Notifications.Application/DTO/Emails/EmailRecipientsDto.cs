// -----------------------------------------------------------------------
// <copyright file="EmailRecipientsDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Represents email recipients dto.
/// </summary>
public class EmailRecipientsDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRecipientsDto"/> class.
    /// </summary>
    public EmailRecipientsDto()
    {
        this.To = new List<EmailAddressDto>();
        this.Cc = new List<EmailAddressDto>();
        this.Bcc = new List<EmailAddressDto>();
    }

    /// <summary>
    /// Gets or sets the 'to' recipients.
    /// </summary>
    public IEnumerable<EmailAddressDto> To { get; set; }

    /// <summary>
    /// Gets or sets the 'cc' recipients.
    /// </summary>
    public IEnumerable<EmailAddressDto> Cc { get; set; }

    /// <summary>
    /// Gets or sets the 'bcc' recipients.
    /// </summary>
    public IEnumerable<EmailAddressDto> Bcc { get; set; }
}
