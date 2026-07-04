// -----------------------------------------------------------------------
// <copyright file="EmailStatusRequestDto.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.DTO.Emails;

/// <summary>
/// Represents email status request dto.
/// </summary>
public class EmailStatusRequestDto
{
    /// <summary>
    /// Gets or sets employee ids.
    /// </summary>
    public List<Guid> EmployeeIds { get; set; } = new List<Guid>();
}
