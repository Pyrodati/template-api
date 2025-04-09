using System.Net;
using System.Text.Json;
using Template.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Template.Presentation.Middlewares;

public class GlobalExceptionHandler(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
            };

            problem.Extensions.Add("error", ex.Error);

            await SetContext(context, problem);
        }
        catch (UnauthorizedException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                Title = "Unauthorized",
            };

            problem.Extensions.Add("error", ex.Error);

            await SetContext(context, problem);
        }
        catch (ForbiddenException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.Forbidden,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                Title = "Forbidden",
            };

            problem.Extensions.Add("error", ex.Error);

            await SetContext(context, problem);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "Not found",
            };

            problem.Extensions.Add("error", ex.Error);

            await SetContext(context, problem);
        }
        catch (ConflictException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.Conflict,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                Title = "Conflict",
            };

            problem.Extensions.Add("error", ex.Error);

            await SetContext(context, problem);
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Exception occured: {Message}", exception.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = " An internal server has occured"
            };
            await SetContext(context, problem);
        }
    }

    private async Task SetContext(HttpContext context, ProblemDetails problemDetails)
    {
        string json = JsonSerializer.Serialize(problemDetails);

        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(json);
    }

}
