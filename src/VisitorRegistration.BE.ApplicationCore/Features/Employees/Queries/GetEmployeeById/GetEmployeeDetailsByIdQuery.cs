// (c) Visitor Registration

using Mapster;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.TransferObjects;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeDetailsByIdQuery (int Id) : IRequest<EmployeeDetailDto?>;

public class GetEmployeeDetailsByIdQueryHandler
    : IRequestHandler<GetEmployeeDetailsByIdQuery, EmployeeDetailDto?>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeDetailsByIdQueryHandler (
        IEmployeeRepository employeeRepository)
    {
        this._employeeRepository = employeeRepository;
    }

    public async Task<EmployeeDetailDto?> Handle (GetEmployeeDetailsByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Employee? employee = await this._employeeRepository
            .GetByIdAsync(request.Id, cancellationToken);

        return employee?.Adapt<EmployeeDetailDto>();
    }
}