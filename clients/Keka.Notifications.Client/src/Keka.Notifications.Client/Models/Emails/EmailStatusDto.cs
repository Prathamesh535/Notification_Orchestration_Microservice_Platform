namespace Keka.Notifications.Client.Models.Emails;

/// <summary>
/// Represents email status dto.
/// </summary>
public class EmailStatusDto
{
    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; }

#nullable enable
    /// <summary>
    /// Gets or sets employee Id.
    /// </summary>
    public Guid? EmployeeId { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the no of emails sent.
    /// </summary>
    public int NoOfEmailsSent { get; set; }

    /// <summary>
    /// Gets or sets the no of emails failed.
    /// </summary>
    public int NoOfEmailsFailed { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the last email was successfully sent.
    /// </summary>
#nullable enable
    public DateTime? LastEmailSentOn { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the last email was successfully delivered.
    /// </summary>
    public DateTime? LastEmailDeliveredOn { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the last email failed to be delivered.
    /// </summary>
    public DateTime? LastEmailFailedOn { get; set; }

    /// <summary>
    /// Gets or sets the reason code for the last email failure.
    /// </summary>
    public short? LastEmailFailedReason { get; set; }
#nullable restore

    /// <summary>
    /// Gets or sets the number of consecutive failed email delivery attempts.
    /// </summary>
    public short ConsecutiveBlockCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email is blocked.
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Gets or sets the  time when the email is last blocked.
    /// </summary>
#nullable enable
    public DateTime? LastBlockedOn { get; set; }

    /// <summary>
    /// Gets or sets the date and time until which the email is blocked.
    /// </summary>
    public DateTime? BlockedUntil { get; set; }
#nullable restore
}