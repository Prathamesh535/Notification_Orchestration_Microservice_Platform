namespace Keka.Notifications.Client.Models.Emails;

/// <summary>
/// Class Email Status Request Dto. 
/// </summary>
public class EmailStatusRequestDto
{
    /// <summary>
    /// Gets or sets the EmployeeIds.
    /// </summary>
   public List<Guid> EmployeeIds { get; set; } = new List<Guid>();
}