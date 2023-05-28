// (c) Visitor Registration

using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.CreateCompany;
using VisitorRegistration.BE.ApplicationCore.Features.Companies.Commands.UpdateCompany;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.CreateEmployee;
using VisitorRegistration.BE.ApplicationCore.Features.Employees.Commands.UpdateEmployee;

namespace VisitorRegistration.BE.ApplicationCore;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices (
        this IServiceCollection services)
    {
        _ = services.AddScoped<IValidator<CreateCompanyCommand>, CreateCompanyCommandValidator>();
        _ = services.AddScoped<IValidator<UpdateCompanyCommand>, UpdateCompanyCommandValidator>();

        _ = services.AddScoped<IValidator<CreateEmployeeCommand>, CreateEmployeeCommandValidator>();
        _ = services.AddScoped<IValidator<UpdateEmployeeCommand>, UpdateEmployeeCommandValidator>();

        _ = services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(
                Assembly.GetExecutingAssembly()));

        return services;
    }
}