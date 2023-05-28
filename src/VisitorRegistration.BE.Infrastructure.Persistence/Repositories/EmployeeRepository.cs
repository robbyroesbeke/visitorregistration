// (c) Visitor Registration

using Microsoft.EntityFrameworkCore;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.Infrastructure.Persistence.EF.Repositories;

internal class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository (VisitorRegistrationDbContext visitorRegistrationDbContext)
        : base(visitorRegistrationDbContext)
    {
    }

    public async ValueTask<IList<Employee>> GetAllForCompanyAsync (int companyId,
        int page,
        int pageSize,
        string? queryFilter,
        CancellationToken cancellationToken)
    {
        IQueryable<Employee> employeesQuery = VisitorRegistrationDbContext
            .Companies
            .Where(company => company.Id == companyId)
            .SelectMany(company => company.Employees);

        if ( queryFilter != null )
        {
            employeesQuery = employeesQuery.Where(employee =>
                employee.FirstName.Contains(queryFilter) ||
                employee.LastName.Contains(queryFilter) ||
                employee.EmailAddress.Contains(queryFilter));
        }

        return await employeesQuery
            .Skip(( page - 1 ) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async ValueTask<Employee?> GetByEmailAddressAsync (
        string emailAddress,
        CancellationToken cancellationToken)
        => await VisitorRegistrationDbContext.Employees
            .FirstOrDefaultAsync(
                employee => employee.EmailAddress.Equals(emailAddress),
                cancellationToken);
}