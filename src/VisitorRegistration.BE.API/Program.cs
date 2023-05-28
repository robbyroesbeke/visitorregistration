// (c) Visitor Registration

using Serilog;

using VisitorRegistration.BE.API.Configuration;
using VisitorRegistration.BE.API.Filter;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogger();

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    _ = options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.AddLayerServices();

builder.AddSwaggerDocumentation();

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();

if ( app.Environment.IsDevelopment() )
{
    app.ConfigureSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

Log.Logger.Information("VisitorRegistration API start up");