// (c) Visitor Registration

using FluentResults;

using FluentValidation;
using FluentValidation.Results;

using Mapster;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.TransferObjects;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand (int EmployeeId,
    EmployeeForUpdateDto EmployeeForUpdateDto) : IRequest<Result>;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<UpdateEmployeeCommand> _updateEmployeeCommandValidator;

    public UpdateEmployeeCommandHandler (IValidator<UpdateEmployeeCommand> updateEmployeeCommandValidator,
        IEmployeeRepository employeeRepository)
    {
        this._updateEmployeeCommandValidator = updateEmployeeCommandValidator
                                               ?? throw new ArgumentNullException(
                                                   nameof(updateEmployeeCommandValidator));
        this._employeeRepository = employeeRepository
                                   ?? throw new ArgumentNullException(nameof(employeeRepository));
    }

    public async Task<Result> Handle (UpdateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        Employee? employee = await this._employeeRepository.GetByIdAsync(
            request.EmployeeId, cancellationToken);

        if ( employee == null )
        {
            return Result.Fail("Employee was not found.");
        }

        ValidationResult validationResult =
            await this._updateEmployeeCommandValidator
                .ValidateAsync(request, cancellationToken);

        if ( !validationResult.IsValid )
        {
            return Result.Fail(
                validationResult.Errors.Select(e => e.ErrorMessage));
        }

        _ = request.EmployeeForUpdateDto.Adapt(employee);
        await this._employeeRepository.UpdateAsync(employee, cancellationToken);

        return Result.Ok();
    }
}