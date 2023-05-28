// (c) Visitor Registration

using FluentValidation;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;

namespace VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandValidator
    : AbstractValidator<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;

    public UpdateCompanyCommandValidator (
        ICompanyRepository companyRepository)
    {
        this._companyRepository = companyRepository
                                  ?? throw new ArgumentNullException(nameof(companyRepository));

        _ = RuleFor(company => company)
            .MustAsync(HaveAUniqueName)
            .WithMessage("A company with the same name already exists.");

        _ = RuleFor(company => company.CompanyForUpdateDto.Name)
            .NotNull()
            .WithMessage("A company must have a name.")
            .NotEmpty()
            .WithMessage("A company must have a name.")
            .MaximumLength(50)
            .WithMessage("A company must have a name, and have a max of 50 characters.");
    }

    private async Task<bool> HaveAUniqueName (
        UpdateCompanyCommand createCompanyCommand,
        CancellationToken cancellationToken)
        => await
               this._companyRepository
                   .GetByNameAsync(
                       createCompanyCommand.CompanyForUpdateDto.Name,
                       cancellationToken)
           == null;
}