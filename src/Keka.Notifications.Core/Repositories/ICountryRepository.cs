// -----------------------------------------------------------------------
// <copyright file="ICountryRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Core.Repositories;

/// <summary>
/// Represents the Interface for the Country Repository.
/// </summary>
public interface ICountryRepository
{
    /// <summary>
    /// Gets the country asynchronously.
    /// </summary>
    /// <param name="countryCode">The app context instance.</param>
    /// <returns>Returns the country.</returns>
    Task<Country> GetCountryByCodeAsync(string countryCode);
}