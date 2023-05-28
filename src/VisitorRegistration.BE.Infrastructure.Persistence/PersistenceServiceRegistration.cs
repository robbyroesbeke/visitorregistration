// (c) Visitor Registration

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using VisitorRegistration.BE.ApplicationCore.Contracts.Repositories;
using VisitorRegistration.BE.Infrastructure.Persistence.EF.Repositories;

namespace VisitorRegistration.BE.Infrastructure.Persistence.EF;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("VisitorRegistrationDbConnection");
        _ = services.AddDbContext<VisitorRegistrationDbContext>(options =>
            options.UseSqlServer(connectionString));

        _ = services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        _ = services.AddScoped<ICompanyRepository, CompanyRepository>();
        _ = services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}