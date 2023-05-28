// (c) Visitor Registration

using FluentResults;

using FluentValidation;
using FluentValidation.Results;

using Mapster;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.TransferObjects;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand (int CompanyId,
    EmployeeForCreationDto EmployeeForCreation) : IRequest<Result<EmployeeDto>>;

public class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, Result<EmployeeDto>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IValidator<CreateEmployeeCommand> _createEmployeeCommandValidator;

    public CreateEmployeeCommandHandler (
        IValidator<CreateEmployeeCommand> createEmployeeCommandValidator,
        ICompanyRepository companyRepository)
    {
        this._createEmployeeCommandValidator = createEmployeeCommandValidator
            ?? throw new ArgumentNullException(nameof(createEmployeeCommandValidator));
        this._companyRepository = companyRepository
            ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<Result<EmployeeDto>> Handle (
        CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidationResult validationResult =
            await this._createEmployeeCommandValidator
                .ValidateAsync(request, cancellationToken);

        if ( !validationResult.IsValid )
        {
            return Result.Fail(
                validationResult.Errors.Select(e => e.ErrorMessage));
        }

        Company? company = await this._companyRepository
            .GetByIdAsync(request.CompanyId, cancellationToken);

        if ( company == null )
        {
            return Result.Fail(
                $"Company with id: {request.CompanyId} was not found");
        }

        Employee employee = request.EmployeeForCreation.Adapt<Employee>();
        company.Employees.Add(employee);

        await this._companyRepository.UpdateAsync(company, cancellationToken);
        return employee.Adapt<EmployeeDto>();
    }
}