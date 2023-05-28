// (c) Visitor Registration

using Mapster;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.TransferObjects;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Companies.Queries.GetCompanyById;

public record GetCompanyDetailsByIdQuery (int Id) : IRequest<CompanyDto?>;

public class GetCompanyDetailsByIdQueryHandler
    : IRequestHandler<GetCompanyDetailsByIdQuery, CompanyDto?>
{
    private readonly IAsyncRepository<Company> _companyRepository;

    public GetCompanyDetailsByIdQueryHandler (
        IAsyncRepository<Company> companyRepository)
    {
        this._companyRepository = companyRepository
                                  ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<CompanyDto?> Handle (
        GetCompanyDetailsByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Company? company = await
            this._companyRepository
                .GetByIdAsync(request.Id, cancellationToken);

        return company?.Adapt<CompanyDto>();
    }
}