// (c) Visitor Registration
// Ignore Spelling: Serilog

using System.Reflection;

using Serilog;

using VisitorRegistration.BE.ApplicationCore;
using VisitorRegistration.BE.Infrastructure.Mail;
using VisitorRegistration.BE.Infrastructure.Persistence.EF;

namespace VisitorRegistration.BE.API.Configuration;

/// <summary>
/// Defines extension methods, that are used in <see cref="Program"/> class.
/// </summary>
public static class ApplicationConfigurationExtensions
{
    /// <summary>
    /// Adds serilog as the logger for the application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <exception cref="System.ArgumentNullException">Throws exception when the web application builder is null</exception>
    public static void AddSerilogLogger (this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.Host.UseSerilog((context, loggerConfiguration)
            => loggerConfiguration
                .WriteTo.Console()
                .ReadFrom.Configuration(context.Configuration));
    }

    /// <summary>
    /// Adds swagger documentation, including XML documentation.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <exception cref="System.ArgumentNullException">Throws exception when the web application builder is null</exception>
    public static void AddSwaggerDocumentation (this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen(setupAction =>
        {
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            setupAction.IncludeXmlComments(xmlCommentsFullPath);
        });
    }

    /// <summary>
    /// Configures the swagger documentation.
    /// </summary>
    /// <param name="webApplication">The web application builder.</param>
    /// <exception cref="System.ArgumentNullException">Throws exception when the web application builder is null</exception>
    public static void ConfigureSwaggerUI (this WebApplication webApplication)
    {
        ArgumentNullException.ThrowIfNull(webApplication);

        _ = webApplication.UseSwagger();
        _ = webApplication.UseSwaggerUI();
    }

    /// <summary>
    /// Adds the different layers of the system. <br />
    /// References the application services (mediator command/queries) <br />
    /// References the mail service <br />
    /// References the persistence services.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <exception cref="System.ArgumentNullException">Throws exception when the web application builder is null</exception>
    public static void AddLayerServices (this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.Services.AddApplicationServices();
        _ = builder.Services.AddMailService(builder.Configuration);
        _ = builder.Services.AddPersistenceServices(builder.Configuration);
    }
}