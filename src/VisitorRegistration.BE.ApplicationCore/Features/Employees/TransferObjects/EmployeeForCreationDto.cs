﻿// (c) Visitor Registration

namespace VisitorRegistration.BE.ApplicationCore.Features.Employees.TransferObjects;

public class EmployeeForCreationDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;
}