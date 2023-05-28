// (c) Visitor Registration

using FluentResults;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.DeleteEmployee;

/// <summary>
/// Defines the command, to delete an employee.
/// <para>
///     The params of the command include the employeeId <br />
///     of the employee that needs to be deleted.
/// </para>
/// </summary>
/// <param name="EmployeeId">The id of the employee that needs to be deleted.</param>
public record DeleteEmployeeCommand (int EmployeeId) : IRequest<Result>;

/// <summary>
/// Defines the command handler for the DeleteEmployeeCommand.
/// </summary>
public class DeleteEmployeeCommandHandler
    : IRequestHandler<DeleteEmployeeCommand, Result>
{
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteEmployeeCommandHandler"/> class.
    /// </summary>
    /// <param name="employeeRepository">The employee repository.</param>
    public DeleteEmployeeCommandHandler (
        IEmployeeRepository employeeRepository)
    {
        this._employeeRepository = employeeRepository
                                   ?? throw new ArgumentNullException(nameof(employeeRepository));
    }

    /// <inheritdoc/>
    public async Task<Result> Handle (DeleteEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        Employee? employee = await this._employeeRepository.GetByIdAsync(
            request.EmployeeId, cancellationToken);

        if ( employee == null )
        {
            return Result.Fail("Employee was not found");
        }

        // TODO: After adding visits
        // This will need to be update to also delete
        // the visits.
        await this._employeeRepository.DeleteAsync(employee, cancellationToken);
        return Result.Ok();
    }
}