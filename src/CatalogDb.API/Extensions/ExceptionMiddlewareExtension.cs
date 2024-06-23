using CatalogDb.API.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatalogDb.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    IExceptionHandlerFeature? contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    int statusCode = contextFeature?.Error switch
                    {
                        ResourceNotFoundException => StatusCodes.Status404NotFound,
                        ArgumentException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    var problemDetails = new ProblemDetails
                    {
                        Status = statusCode,
                        Title = ((HttpStatusCode)statusCode).ToString(),
                        Detail = contextFeature?.Error.Message,
                        Instance = contextFeature?.Path
                    };

                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(problemDetails);
                });
            });
        }
    }
}
