// (c) Visitor Registration

using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;

public interface IEmployeeRepository
    : IAsyncRepository<Employee>
{
    ValueTask<IList<Employee>> GetAllForCompanyAsync (
        int companyId, int page, int pageSize, string? queryFilter, CancellationToken cancellationToken);

    ValueTask<Employee?> GetByEmailAddressAsync (
        string emailAddress, CancellationToken cancellationToken);
}