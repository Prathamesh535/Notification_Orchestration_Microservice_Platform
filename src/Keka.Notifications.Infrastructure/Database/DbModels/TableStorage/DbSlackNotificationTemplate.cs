// -----------------------------------------------------------------------
// <copyright file="DbSlackNotificationTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

internal class DbSlackNotificationTemplate : TableEntity
{
    public Guid SlackNotificationTemplateId { get; set; }
    public string Name { get; set; }
    public string Body { get; set; }
}
