// (c) Visitor Registration

using FluentResults;

using FluentValidation;
using FluentValidation.Results;

using Mapster;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.TransferObjects;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.CreateCompany;

public record CreateCompanyCommand (CompanyForCreationDto CompanyForCreationDto)
    : IRequest<Result<CompanyDto>>;

public class CreateCompanyCommandHandler
    : IRequestHandler<CreateCompanyCommand, Result<CompanyDto>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IValidator<CreateCompanyCommand> _createCompanyCommandValidator;

    public CreateCompanyCommandHandler (
        IValidator<CreateCompanyCommand> createCompanyCommandValidator,
        ICompanyRepository companyRepository)
    {
        this._createCompanyCommandValidator = createCompanyCommandValidator
                                              ?? throw new ArgumentNullException(nameof(createCompanyCommandValidator));
        this._companyRepository = companyRepository
                                  ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<Result<CompanyDto>> Handle (
        CreateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidationResult validationResult =
            await this._createCompanyCommandValidator
                .ValidateAsync(request, cancellationToken);

        if ( !validationResult.IsValid )
        {
            return Result.Fail(
                validationResult.Errors.Select(e => e.ErrorMessage));
        }

        Company companyToCreate = request.CompanyForCreationDto.Adapt<Company>();
        _ = await this._companyRepository.AddAsync(companyToCreate, cancellationToken);

        CompanyDto companyDto = companyToCreate.Adapt<CompanyDto>();
        return Result.Ok(companyDto);
    }
}