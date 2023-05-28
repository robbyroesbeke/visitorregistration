// (c) Visitor Registration

using Mapster;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.TransferObjects;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.Queries.GetAllEmployees;

public record GetAllEmployeesPaginatedQuery (int CompanyId, int Page, int PageSize, string? QueryFilter)
    : IRequest<List<EmployeeDto>>;

public class GetAllEmployeesPaginatedQueryHandler
    : IRequestHandler<GetAllEmployeesPaginatedQuery, List<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesPaginatedQueryHandler (IEmployeeRepository employeeRepository)
    {
        this._employeeRepository = employeeRepository;
    }

    public async Task<List<EmployeeDto>> Handle (GetAllEmployeesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IList<Employee> employees = await this._employeeRepository
            .GetAllForCompanyAsync(request.CompanyId,
                request.Page,
                request.PageSize,
                request.QueryFilter,
                cancellationToken);

        return employees.Adapt<List<EmployeeDto>>();
    }
}