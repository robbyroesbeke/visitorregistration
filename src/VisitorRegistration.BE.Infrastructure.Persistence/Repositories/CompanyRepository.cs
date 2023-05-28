// (c) Visitor Registration

using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.Infrastructure.Persistence.EF.Repositories;

internal class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository (VisitorRegistrationDbContext visitorRegistrationDbContext)
        : base(visitorRegistrationDbContext)
    {
    }

    public async ValueTask<IList<Company>> GetAllAsync (int page,
        int pageSize,
        string? queryFilter,
        CancellationToken cancellationToken)
    {
        Expression<Func<Company, bool>>? predicate = null;
        if ( queryFilter != null )
        {
            predicate = company => company.Name.Contains(queryFilter);
        }

        return await base.GetAllAsync(page, pageSize, predicate, cancellationToken);
    }

    public async ValueTask<Company?> GetByNameAsync
        (string companyName, CancellationToken cancellationToken)
        => await VisitorRegistrationDbContext.Set<Company>()
            .FirstOrDefaultAsync(
                company => company.Name.Equals(companyName),
                cancellationToken);

    public async ValueTask DeleteAllEmployeesAsync (int companyId, CancellationToken cancellationToken)
    {
        IQueryable<Employee> employees = VisitorRegistrationDbContext.Set<Company>()
            .Where(company => company.Id == companyId)
            .Include(company => company.Employees)
            .SelectMany(company => company.Employees);

        VisitorRegistrationDbContext.Employees.RemoveRange(employees);
        _ = await VisitorRegistrationDbContext.SaveChangesAsync(cancellationToken);
    }
}