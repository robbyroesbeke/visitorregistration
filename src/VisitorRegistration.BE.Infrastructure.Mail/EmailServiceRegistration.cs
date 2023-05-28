// (c) Visitor Registration

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using VisitorRegistration.BE.ApplicationCore.Contracts.ContractModels.MailService;
using VisitorRegistration.BE.ApplicationCore.Contracts.Infrastructure;
using VisitorRegistration.BE.Infrastructure.Mail.Mail;

namespace VisitorRegistration.BE.Infrastructure.Mail;

public static class EmailServiceRegistration
{
    public static IServiceCollection AddMailService (this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        IConfigurationSection emailSettings = configuration.GetSection("EmailSettings");
        _ = services.Configure<EmailSettings>(emailSettings);
        _ = services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}