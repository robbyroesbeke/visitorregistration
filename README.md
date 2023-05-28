# Visitor Registration System

## Functional
A simple application to register visitors who enter a building. When entering the office building, visitors must register themselves at the front desk. Upon exiting the building, the visitors must sign out at the front desk.

## Requirements

### Sign In
Visitors must register with the following data:
- Name
- Email
- Company
- Visiting company (the company is selected from a list)
- Appointment with (based on the selected company, a list of employees is shown)

### Sign out
When leaving, visitors must sign out based on the email of the visitor and visitors that are currently in the building.

### Administration panel
In the administration panel, the user should be able to:
- Manage the list of companies
- Manage the list of employees of a company
- See a list of visitors currently in the building
- Perform a basic search of all the registrations based on:
  - Visitor name
  - Visitor email
  - Visiting company
  - Appointment with

Each registration must have a start and end date.

## Technical

### NuGet Packages

- FluentResults (https://www.nuget.org/packages/FluentResults)
- FluentValidation (https://www.nuget.org/packages/FluentValidation)
- Mapster (https://www.nuget.org/packages/Mapster)
- MediatR (https://www.nuget.org/packages/MediatR)
- SonarAnalyzer.CSharp (https://www.nuget.org/packages/SonarAnalyzer.CSharp)

    => Installed with Directory.Build.props

### Architectural patterns

- Clean architecture
- CQRS

### Helpful courses
- [Gill Cleeren - ASP.NET CORE 6 Clean Architecture](https://app.pluralsight.com/library/courses/asp-dot-net-core-6-clean-architecture/table-of-contents)

## Things I learned
Building this simple application taught me a few things:
- Using `SelectMany(parent => parent.ChildCollection)`, we can get the child collection without loading everything into memory. This is useful when we want to query the 'childcollection' and we need to filter on the parent before filtering on the children or when we are using aggregates (DDD).
