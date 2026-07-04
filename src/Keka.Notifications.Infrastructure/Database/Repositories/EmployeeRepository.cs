// -----------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Database.Repositories;

/// <summary>
/// Represents the employee repository.
/// </summary>
/// <seealso cref="BaseRepository" />
/// <seealso cref="Keka.Notifications.Core.Repositories.IEmployeeRepository" />
internal class EmployeeRepository(DatabaseContext db, IMapper mapper, IAppContext appContext)
    : BaseRepository(db, mapper, appContext), IEmployeeRepository
{
    /// <inheritdoc/>
    public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId)
    {
        var dbEmployee = await this.Db.Connection.QuerySingleOrDefaultAsync<DbEmployee>(EmployeeQueries.SelectEmployees, new { employeeId }, this.Db.Transaction);
        return this.Mapper.Map<Employee>(dbEmployee);
    }

    /// <inheritdoc/>
    public async Task AddEmployeeAsync(Employee employee)
    {
        var dbEmployee = this.Mapper.Map<DbEmployee>(employee);
        dbEmployee.SetAuditFieldsOnCreate(this.AppContext);

        await this.Db.Connection.ExecuteAsync(EmployeeQueries.InsertEmployee, dbEmployee);
    }
}
