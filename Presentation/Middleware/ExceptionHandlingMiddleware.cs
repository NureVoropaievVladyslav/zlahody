using System.Net;
using System.Text.Json;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        logger.LogError(ex, ex.Message);

        var httpStatusCode = ex is ExceptionBase exceptionBase 
            ? exceptionBase.HttpStatusCode 
            : HttpStatusCode.InternalServerError;
        
        var problem = new ProblemDetails()
        {
            Status = (int)httpStatusCode,
            Type = httpStatusCode.ToString(),
            Detail = ex.Message
        };

        var problemJson = JsonSerializer.Serialize(problem);
        context.Response.StatusCode = (int)httpStatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(problemJson);
    }
}