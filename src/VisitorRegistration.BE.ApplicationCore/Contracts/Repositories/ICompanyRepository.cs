// (c) Visitor Registration

using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;

public interface ICompanyRepository : IAsyncRepository<Company>
{
    ValueTask<IList<Company>> GetAllAsync (
        int page, int pageSize, string? queryFilter, CancellationToken cancellationToken);

    ValueTask<Company?> GetByNameAsync (
        string companyName, CancellationToken cancellationToken);

    ValueTask DeleteAllEmployeesAsync (
        int companyId, CancellationToken cancellationToken);
}