// (c) Visitor Registration

using FluentResults;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.CreateCompany;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.DeleteCompany;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.UpdateCompany;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.Queries.GetAllCompanies;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.Queries.GetCompanyById;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.TransferObjects;

namespace VisitorRegistration.BE.API.Controllers;

[ApiController]
[Route("api/company")]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyController (IMediator mediator)
    {
        this._mediator = mediator
                         ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    ///     Gets all companies, paginated.
    /// </summary>
    /// <param name="page">The page number of the paginated list.</param>
    /// <param name="pageSize">The page size of the page.</param>
    /// <param name="queryString">The query string.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of companies, if non are found an empty list will be returned.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll ([FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? queryString = null,
        CancellationToken cancellationToken = default!)
    {
        List<CompanyDto> response = await
            this._mediator.Send(
                new GetAllCompaniesPaginatedQuery(page, pageSize, queryString), cancellationToken);

        return Ok(response);
    }

    /// <summary>
    ///     Get a company by Id.
    /// </summary>
    /// <param name="companyId">The company Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpGet("{companyId:int}", Name = "GetCompanyById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompanyById (int companyId,
        CancellationToken cancellationToken)
    {
        CompanyDto? response = await
            this._mediator.Send(
                new GetCompanyDetailsByIdQuery(companyId), cancellationToken);

        return response == null
            ? NotFound()
            : Ok(response);
    }

    /// <summary>
    ///     Create a new company.
    /// </summary>
    /// <param name="companyForCreation">The details of the to be created company.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create ([FromBody] CompanyForCreationDto companyForCreation,
        CancellationToken cancellationToken)
    {
        Result<CompanyDto> response = await
            this._mediator.Send(
                new CreateCompanyCommand(companyForCreation), cancellationToken);

        return response.IsFailed
            ? BadRequest(response.Errors.Select(e => e.Message))
            : CreatedAtAction("GetCompanyById", new { companyId = response.Value.Id }, response.Value);
    }

    /// <summary>
    ///     Update an existing company. <br />
    ///     When the provided Id references a company that does not exists,
    ///     a BadRequest will be returned.
    /// </summary>
    /// <param name="companyId">The companyId of the company that we want to update.</param>
    /// <param name="companyForUpdate">The new details of the company.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpPut("{companyId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update (int companyId,
        [FromBody] CompanyForUpdateDto companyForUpdate,
        CancellationToken cancellationToken)
    {
        Result response = await
            this._mediator.Send(
                new UpdateCompanyCommand(companyId, companyForUpdate), cancellationToken);

        return response.IsFailed
            ? BadRequest(response.Errors.Select(e => e.Message))
            : NoContent();
    }

    /// <summary>
    /// Delete the company, using the companyId.
    /// </summary>
    /// <param name="companyId">The company id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpDelete("{companyId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete (int companyId,
        CancellationToken cancellationToken)
    {
        Result response = await
            this._mediator.Send(
                new DeleteCompanyCommand(companyId), cancellationToken);

        return response.IsFailed
            ? BadRequest(response.Errors.Select(e => e.Message))
            : NoContent();
    }
}