// -----------------------------------------------------------------------
// <copyright file="CountryRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Represents Country repository.
/// </summary>
/// <param name="db">The Database Context.</param>
/// <param name="mapper">The automapper instance.</param>
/// <param name="appContext">The app context.</param>
internal class CountryRepository(DatabaseContext db, IMapper mapper, IAppContext appContext)
    : BaseRepository(db, mapper, appContext), ICountryRepository
{
    /// <inheritdoc/>
    public async Task<Country> GetCountryByCodeAsync(string countryCode)
    {
        var country = await this.Db.Connection.QueryFirstOrDefaultAsync<DbCountry>(CountryQueries.GetCountryByCode, new { CountryCode = countryCode });
        return this.Mapper.Map<Country>(country);
    }
}