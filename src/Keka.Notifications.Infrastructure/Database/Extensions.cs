// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database;

/// <summary>
/// Database extensions to upload audit fields when entity is created, updated and deleted.
/// </summary>
internal static class Extensions
{
    /// <summary>
    /// Set audit fields on create.
    /// </summary>
    /// <param name="entity">The database entity.</param>
    /// <param name="appContext">The app content.</param>
    internal static void SetAuditFieldsOnCreate(this DbBaseModel entity, IAppContext appContext)
    {
        var now = DateTime.UtcNow;
        entity.TenantId = appContext.TenantId;
        entity.CreatedOn = now;
        entity.CreatedBy = appContext.UserId;
    }

    /// <summary>
    /// Set audit fields on create.
    /// </summary>
    /// <param name="entities">The list of database entity.</param>
    /// <param name="appContext">The app content.</param>
    internal static void SetAuditFieldsOnCreate(this IEnumerable<DbBaseModel> entities, IAppContext appContext)
    {
        var now = DateTime.UtcNow;
        foreach (var entity in entities)
        {
            entity.TenantId = appContext.TenantId;
            entity.CreatedOn = now;
            entity.CreatedBy = appContext.UserId;
        }
    }

    /// <summary>
    /// Set audit fields on update.
    /// </summary>
    /// <param name="entity">The database entity.</param>
    /// <param name="appContext">The app context.</param>
    internal static void SetAuditFieldsOnUpdate(this DbBaseModel entity, IAppContext appContext)
    {
        var now = DateTime.UtcNow;
        entity.UpdatedOn = now;
        entity.UpdatedBy = appContext.UserId;
    }

    /// <summary>
    /// Set audit fields on update.
    /// </summary>
    /// <param name="entities">The list of database entity.</param>
    /// <param name="appContext">The app content.</param>
    internal static void SetAuditFieldsOnUpdate(this IEnumerable<DbBaseModel> entities, IAppContext appContext)
    {
        var now = DateTime.UtcNow;
        foreach (var entity in entities)
        {
            entity.UpdatedOn = now;
            entity.UpdatedBy = appContext.UserId;
        }
    }

    /// <summary>
    /// Set audit fields on delete.
    /// </summary>
    /// <param name="entity">The database entity.</param>
    /// <param name="appContext">The app context.</param>
    internal static void SetAuditFieldsOnDelete(this DbBaseModel entity, IAppContext appContext)
    {
        var now = DateTime.UtcNow;
        entity.IsDeleted = true;
        entity.DeletedOn = now;
        entity.DeletedBy = appContext.UserId;
    }

    /// <summary>
    /// Set audit fields on delete.
    /// </summary>
    /// <param name="entities">The list of database entity.</param>
    /// <param name="appContext">The app content.</param>
    internal static void SetAuditFieldsOnDelete(this IEnumerable<DbBaseModel> entities, IAppContext appContext)
    {
        var now = DateTime.UtcNow;
        foreach (var entity in entities)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = now;
            entity.DeletedBy = appContext.UserId;
        }
    }
}
