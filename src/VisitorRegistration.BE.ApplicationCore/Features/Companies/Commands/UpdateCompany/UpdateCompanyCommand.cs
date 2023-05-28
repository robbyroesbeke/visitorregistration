// (c) Visitor Registration

using FluentResults;

using FluentValidation;
using FluentValidation.Results;

using Mapster;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.UpdateCompany;

public record UpdateCompanyCommand (int Id,
    CompanyForUpdateDto CompanyForUpdateDto) : IRequest<Result>;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Result>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IValidator<UpdateCompanyCommand> _updateCompanyCommandValidator;

    public UpdateCompanyCommandHandler (
        IValidator<UpdateCompanyCommand> updateCompanyCommandValidator,
        ICompanyRepository companyRepository)
    {
        this._updateCompanyCommandValidator = updateCompanyCommandValidator
                                              ?? throw new ArgumentNullException(nameof(updateCompanyCommandValidator));
        this._companyRepository = companyRepository
                                  ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<Result> Handle (
        UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        Company? company = await this._companyRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if ( company == null )
        {
            return Result.Fail("Company was not found.");
        }

        ValidationResult validationResult =
            await this._updateCompanyCommandValidator
                .ValidateAsync(request, cancellationToken);

        if ( !validationResult.IsValid )
        {
            return Result.Fail(
                validationResult.Errors.Select(e => e.ErrorMessage));
        }

        _ = request.CompanyForUpdateDto.Adapt(company);
        await this._companyRepository.UpdateAsync(company, cancellationToken);

        return Result.Ok();
    }
}