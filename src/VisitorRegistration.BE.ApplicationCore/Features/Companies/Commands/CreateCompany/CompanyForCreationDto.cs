// (c) Visitor Registration
// Ignore Spelling: Dto

namespace VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.CreateCompany;

/// <summary>
///     The details needed to create a new company.
/// </summary>
public class CompanyForCreationDto
{
    /// <summary>
    ///     Gets or sets the name for the company.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}