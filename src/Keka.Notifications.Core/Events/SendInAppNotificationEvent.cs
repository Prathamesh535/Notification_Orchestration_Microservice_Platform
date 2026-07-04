// -----------------------------------------------------------------------
// <copyright file="SendInAppNotificationEvent.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Events;

/// <summary>
/// Represents new in-app notification send event.
/// </summary>
public record SendInAppNotificationEvent : TenantEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendInAppNotificationEvent"/> class.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="employeeId">The in-app notification recipient employee id.</param>
    /// <param name="inAppNotificationId">The in-app notification id.</param>
    public SendInAppNotificationEvent(Guid tenantId, Guid userId, Guid employeeId, string inAppNotificationId)
        : base(tenantId, userId)
    {
        this.EmployeeId = employeeId;
        this.InAppNotificationId = inAppNotificationId;
    }

    /// <summary>
    /// Gets the in-app notification employee identifier.
    /// </summary>
    public Guid EmployeeId { get; }

    /// <summary>
    /// Gets the in-app notification identifier.
    /// </summary>
    public string InAppNotificationId { get; }
}
