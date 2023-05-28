// (c) Visitor Registration

using VisitorRegistration.BE.Core.Common;

namespace VisitorRegistration.BE.Core.Entities;

public class Company : AudibleEntity
{
    public string Name { get; set; } = "Unknown Company";

    public IList<Employee> Employees { get; set; } = new List<Employee>();
}