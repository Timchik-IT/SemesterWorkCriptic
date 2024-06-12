using System.Net;
using Newtonsoft.Json;
using Serilog;

namespace Criptic.API.Middlewares;

public record ExceptionsCatcherMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        string errorMessage;

        switch (exception)
        {
            // кастомные ошибки
            
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorMessage = "An unexpected error occurred.";
                break;
        }
        
        LogException(context, exception, statusCode, errorMessage);
        var result = JsonConvert.SerializeObject(new { error = errorMessage });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(result);
    }

    private static void LogException(HttpContext context, Exception exception, HttpStatusCode statusCode,
        string errorMessage)
    {
        var logMessage = $"""
                              Exception: {exception.GetType().Name}
                              Message: {exception.Message}
                              Status Code: {(int)statusCode} ({statusCode})
                              Path: {context.Request.Path}
                              Method: {context.Request.Method}
                              Query: {context.Request.QueryString}
                              Stack Trace: {exception.StackTrace}
                              Error Message: {errorMessage}
                          """;
        Console.Error.WriteLine(logMessage);
        Log.Error(logMessage);
    }
}