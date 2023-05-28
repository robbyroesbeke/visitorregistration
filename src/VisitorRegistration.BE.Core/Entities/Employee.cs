// (c) Visitor Registration

using VisitorRegistration.BE.Core.Common;

namespace VisitorRegistration.BE.Core.Entities;

public class Employee : AudibleEntity
{
    public string FirstName { get; set; } = "John";

    public string LastName { get; set; } = "Doe";

    public string Function { get; set; } = "Trainee";

    public string EmailAddress { get; set; } = "john.doe@company.eu";
}