namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Initializes a new instance of the <see cref="EmployeeNotificationPreferenceRepository"/> class.
/// </summary>
/// <param name="db">The database context to be used by the repository.</param>
/// <param name="mapper">The mapper for converting between domain and database models.</param>
/// <param name="appContext">The application context containing information about the current user and environment.</param>
/// <seealso cref="Keka.Notifications.Infrastructure.Database.Repositories.BaseRepository" />
/// <seealso cref="Keka.Notifications.Core.Repositories.IEmployeeNotificationPreferenceRepository" />
internal class EmployeeNotificationPreferenceRepository(DatabaseContext db, IMapper mapper, IAppContext appContext)
    : BaseRepository(db, mapper, appContext), IEmployeeNotificationPreferenceRepository
{
    /// <inheritdoc />
    public async Task<int> InsertEmployeeNotificationPreferencesAsync(List<EmployeeNotificationPreference> employeeNotificationPreferences)
    {
        var dbEmployeeNotificationPreferences = this.Mapper.Map<List<DbEmployeeNotificationPreference>>(employeeNotificationPreferences);

        dbEmployeeNotificationPreferences.ForEach(pref =>
        {
            pref.SetAuditFieldsOnCreate(this.AppContext);
        });

        return await this.Db.Connection.ExecuteAsync(EmployeeNotificationPreferenceQueries.InsertEmployeeNotificationPreference, dbEmployeeNotificationPreferences);
    }

    /// <inheritdoc />
    public async Task<List<EmployeeNotificationPreference>> GetDisabledEmployeeNotificationPreferencesAsync(Guid employeeId)
    {
        var employeeNotificationPreferences = (await this.Db.Connection.QueryAsync<DbEmployeeNotificationPreference>(
            EmployeeNotificationPreferenceQueries.GetDisabledEmployeeNotificationPreferences,
            new { EmployeeId = employeeId })).ToList();

        return this.Mapper.Map<List<EmployeeNotificationPreference>>(employeeNotificationPreferences);
    }

    /// <inheritdoc />
    public async Task<List<EmployeeNotificationPreference>> GetEmployeeNotificationPreferencesAsync(Guid employeeId)
    {
        var employeeNotificationPreferences = (await this.Db.Connection.QueryAsync<NotificationPreferenceEventInfo>(
            EmployeeNotificationPreferenceQueries.GetEmployeeNotificationPreferences,
            new { EmployeeId = employeeId })).ToList();
        return this.Mapper.Map<List<EmployeeNotificationPreference>>(employeeNotificationPreferences);
    }

    /// <inheritdoc />
    public async Task<int> DeleteEmployeeNotificationPreferencesAsync(List<EmployeeNotificationPreference> employeeNotificationPreferences)
    {
        var dbEmployeeNotificationPreferences = this.Mapper.Map<List<DbEmployeeNotificationPreference>>(employeeNotificationPreferences);
        return await this.Db.Connection.ExecuteAsync(EmployeeNotificationPreferenceQueries.DeleteEmployeeNotificationPreferences, dbEmployeeNotificationPreferences);
    }
}