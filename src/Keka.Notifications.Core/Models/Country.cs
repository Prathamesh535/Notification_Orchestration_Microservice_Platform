// -----------------------------------------------------------------------
// <copyright file="Country.cs" company="Keka Inc">
// Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Models;

/// <summary>
/// Represents an Country Information Containing Information Such as CountryId,CountryName,CountryCode etc.
/// </summary>
public class Country
{
    /// <summary>
    /// Gets or sets the Country Id.
    /// </summary>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Gets or sets the Name of the Country.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Country Code.
    /// </summary>
    public string CountryCode { get; set; }

    /// <summary>
    /// Gets or sets from phone number.
    /// </summary>
    public string FromPhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether country is Enabled for Sms or not.
    /// </summary>
    public bool IsSmsEnabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether country is Enabled for WhatsApp or not.
    /// </summary>
    public bool IsWaEnabled { get; set; }
}
