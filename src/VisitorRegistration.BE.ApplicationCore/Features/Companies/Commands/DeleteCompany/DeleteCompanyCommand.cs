// (c) Visitor Registration

using FluentResults;

using MediatR;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.DeleteCompany;

public record DeleteCompanyCommand (int Id) : IRequest<Result>;

public class DeleteCompanyCommandHandler
    : IRequestHandler<DeleteCompanyCommand, Result>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyCommandHandler (
        ICompanyRepository _companyRepository)
    {
        this._companyRepository = _companyRepository;
    }

    public async Task<Result> Handle (
        DeleteCompanyCommand request,
        CancellationToken cancellationToken)
    {
        Company? companyFromDatabase =
            await this._companyRepository
                .GetByIdAsync(request.Id, cancellationToken);

        if ( companyFromDatabase == null )
        {
            return Result.Fail($"Company with id: {request.Id} not found");
        }

        // TODO: delete all visits for all the given employees
        await this._companyRepository.DeleteAllEmployeesAsync(request.Id, cancellationToken);
        await this._companyRepository.DeleteAsync(companyFromDatabase, cancellationToken);

        return Result.Ok();
    }
}