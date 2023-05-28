// (c) Visitor Registration

using Mapster;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.TransferObjects;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Companies.Queries.GetAllCompanies;

public record GetAllCompaniesPaginatedQuery (int Page, int PageSize, string? QueryFilter)
    : IRequest<List<CompanyDto>>;

public class GetAllCompaniesPaginatedQueryHandler
    : IRequestHandler<GetAllCompaniesPaginatedQuery, List<CompanyDto>>
{
    private readonly ICompanyRepository _companyRepository;

    public GetAllCompaniesPaginatedQueryHandler (
        ICompanyRepository companyRepository)
    {
        this._companyRepository = companyRepository
                                  ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<List<CompanyDto>> Handle (
        GetAllCompaniesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IList<Company> companies = await this._companyRepository
            .GetAllAsync(request.Page,
                request.PageSize,
                request.QueryFilter,
                cancellationToken);

        return companies.Adapt<List<CompanyDto>>();
    }
}