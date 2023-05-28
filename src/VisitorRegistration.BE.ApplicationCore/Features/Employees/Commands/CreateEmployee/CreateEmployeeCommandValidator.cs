// (c) Visitor Registration

using FluentValidation;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator
    : AbstractValidator<CreateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandValidator (IEmployeeRepository employeeRepository)
    {
        this._employeeRepository = employeeRepository
                                   ?? throw new ArgumentNullException(nameof(employeeRepository));

        _ = RuleFor(employee => employee)
            .MustAsync(HaveAUniqueEmailAddress)
            .WithMessage("An employee with the same email address already exists.");

        _ = RuleFor(employee => employee.EmployeeForCreation.FirstName)
            .NotNull()
            .WithMessage("An employee must have a first name.")
            .NotEmpty()
            .WithMessage("An employee must have a first name.")
            .MaximumLength(20)
            .WithMessage("An employee must have a first name, that does not exceed 20 characters.");

        _ = RuleFor(company => company.EmployeeForCreation.LastName)
            .NotNull()
            .WithMessage("An employee must have a last name.")
            .NotEmpty()
            .WithMessage("An employee must have a last name.")
            .MaximumLength(30)
            .WithMessage("An employee must have a last name, that does not exceed 30 characters.");

        _ = RuleFor(company => company.EmployeeForCreation.EmailAddress)
            .NotNull()
            .WithMessage("An employee must have an email address.")
            .NotEmpty()
            .WithMessage("An employee must have an email address.")
            .MaximumLength(75)
            .WithMessage("An employee must have an email address, that does not exceed 75 characters.");
    }

    private async Task<bool> HaveAUniqueEmailAddress (
        CreateEmployeeCommand createEmployeeCommand,
        CancellationToken cancellationToken)
        => await
               this._employeeRepository
                   .GetByEmailAddressAsync(
                       createEmployeeCommand.EmployeeForCreation.EmailAddress,
                       cancellationToken)
           == null;
}