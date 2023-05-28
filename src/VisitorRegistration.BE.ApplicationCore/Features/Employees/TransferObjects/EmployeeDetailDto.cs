// (c) Visitor Registration

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.TransferObjects;

public class EmployeeDetailDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Function { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;
}