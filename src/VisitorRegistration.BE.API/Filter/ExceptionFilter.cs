// (c) Visitor Registration

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VisitorRegistration.BE.API.Filter;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _exceptionFilterLogger;

    public ExceptionFilter (
        ILogger<ExceptionFilter> exceptionFilterLogger)
    {
        this._exceptionFilterLogger
            = exceptionFilterLogger ?? throw new ArgumentNullException(nameof(exceptionFilterLogger));
    }

    public void OnException (ExceptionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        Exception? exception = context.Exception;
        if ( exception != null )
        {
            ExceptionLogging.ExceptionOccurred(
                this._exceptionFilterLogger,
                exception.ToString());
        }

        context.Result = new ObjectResult("Out of coffee, I'll get back to you after a refill.")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        context.ExceptionHandled = true;
    }
}