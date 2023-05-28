// (c) Visitor Registration

namespace VisitorRegistration.BE.API.Filter;

public static partial class ExceptionLogging
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Error,
        Message = "Exception occurred `{message}`")]
    public static partial void ExceptionOccurred (ILogger logger, string message);
}