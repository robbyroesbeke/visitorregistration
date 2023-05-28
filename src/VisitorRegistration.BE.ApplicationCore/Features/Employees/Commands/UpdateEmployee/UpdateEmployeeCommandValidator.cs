// (c) Visitor Registration
// Ignore Spelling: Validator

using FluentValidation;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator
    : AbstractValidator<UpdateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandValidator (IEmployeeRepository employeeRepository)
    {
        this._employeeRepository = employeeRepository
                                   ?? throw new ArgumentNullException(nameof(employeeRepository));

        _ = RuleFor(employee => employee)
            .MustAsync(HaveAUniqueEmailAddress)
            .WithMessage("An employee with the same email address already exists.");

        _ = RuleFor(employee => employee.EmployeeForUpdateDto.FirstName)
            .NotNull()
            .WithMessage("An employee must have a first name.")
            .NotEmpty()
            .WithMessage("An employee must have a first name.")
            .MaximumLength(20)
            .WithMessage("An employee must have a first name, that does not exceed 20 characters.");

        _ = RuleFor(company => company.EmployeeForUpdateDto.LastName)
            .NotNull()
            .WithMessage("An employee must have a last name.")
            .NotEmpty()
            .WithMessage("An employee must have a last name.")
            .MaximumLength(30)
            .WithMessage("An employee must have a last name, that does not exceed 30 characters.");

        _ = RuleFor(company => company.EmployeeForUpdateDto.EmailAddress)
            .NotNull()
            .WithMessage("An employee must have an email address.")
            .NotEmpty()
            .WithMessage("An employee must have an email address.")
            .MaximumLength(75)
            .WithMessage("An employee must have an email address, that does not exceed 75 characters.");
        this._employeeRepository = employeeRepository;
    }

    private async Task<bool> HaveAUniqueEmailAddress (
        UpdateEmployeeCommand updateEmployeeCommand,
        CancellationToken cancellationToken)
        => await
               this._employeeRepository
                   .GetByEmailAddressAsync(
                       updateEmployeeCommand.EmployeeForUpdateDto.EmailAddress,
                       cancellationToken)
           == null;
}