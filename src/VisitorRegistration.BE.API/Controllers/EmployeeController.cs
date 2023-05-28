// (c) Visitor Registration

using FluentResults;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.CreateEmployee;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.DeleteEmployee;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.UpdateEmployee;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.Queries.GetAllEmployees;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.Queries.GetEmployeeById;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.TransferObjects;

namespace VisitorRegistration.BE.API.Controllers;

[ApiController]
[Route("/api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController (IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    [Route("/api/company/{companyId:int}/employees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll (int companyId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? queryString = null,
        CancellationToken cancellationToken = default!)
    {
        List<EmployeeDto> response = await
            this._mediator.Send(
                new GetAllEmployeesPaginatedQuery(companyId, page, pageSize, queryString), cancellationToken);

        return Ok(response);
    }

    [HttpGet("{employeeId:int}", Name = "GetEmployeeById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeById (int employeeId,
        CancellationToken cancellationToken = default!)
    {
        EmployeeDetailDto? response = await
            this._mediator.Send(
                new GetEmployeeDetailsByIdQuery(employeeId), cancellationToken);

        return response == null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost]
    [Route("/api/company/{companyId:int}/employees")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create (int companyId,
        [FromBody] EmployeeForCreationDto employeeForCreation,
        CancellationToken cancellationToken)
    {
        Result<EmployeeDto> response = await
            this._mediator.Send(
                new CreateEmployeeCommand(companyId, employeeForCreation), cancellationToken);

        return response.IsFailed
            ? BadRequest(response.Errors.Select(e => e.Message))
            : CreatedAtRoute("GetEmployeeById", new { companyId, employeeId = response.Value.Id }, response.Value);
    }

    [HttpPut("{employeeId:int}")]
    public async Task<IActionResult> Update (int employeeId,
        [FromBody] EmployeeForUpdateDto employeeForUpdate,
        CancellationToken cancellationToken)
    {
        Result response = await
            this._mediator.Send(
                new UpdateEmployeeCommand(employeeId, employeeForUpdate), cancellationToken);

        return response.IsFailed
            ? BadRequest(response.Errors.Select(e => e.Message))
            : NoContent();
    }

    [HttpDelete("{employeeId:int}")]
    public async Task<IActionResult> Delete (int employeeId,
        CancellationToken cancellationToken)
    {
        Result response = await
            this._mediator.Send(
                new DeleteEmployeeCommand(employeeId), cancellationToken);

        return response.IsFailed
            ? BadRequest(response.Errors.Select(e => e.Message))
            : NoContent();
    }
}