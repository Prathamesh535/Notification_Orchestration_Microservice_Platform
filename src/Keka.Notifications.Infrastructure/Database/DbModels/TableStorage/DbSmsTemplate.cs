// -----------------------------------------------------------------------
// <copyright file="DbSmsTemplate.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Keka.Notifications.Infrastructure.Database.DbModels.TableStorage;

/// <summary>
/// Represents an Sms Template Containing TemplateId,ExternalTemplateId,ParameterMapping etc.
/// </summary>
internal class DbSmsTemplate : TableEntity
{
    /// <summary>
    /// Gets or sets the sms template Id.
    /// </summary>
    public Guid SmsTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the External Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the External Template Id.
    /// </summary>
    public string ExternalTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the Parameter Mapping.
    /// </summary>
    public string ParameterMapping { get; set; }
}
