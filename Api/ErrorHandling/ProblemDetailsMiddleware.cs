using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.ErrorHandling
{
    public class ProblemDetailsMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly JsonSerializerOptions _options;

        public ProblemDetailsMiddleware(ILogger<ProblemDetailsMiddleware> logger)
        {
            _logger = logger;
            _options = new JsonSerializerOptions { WriteIndented = true };
            _options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ProblemDetailsException exception)
            {
                context.Response.Clear();
                context.Response.ContentType = MediaTypeNames.Application.Json;

                if (exception.ProblemDetails.Status != null)
                {
                    context.Response.StatusCode = exception.ProblemDetails.Status.Value;
                }

                _logger.LogError($"A Problem has occured:\n statuscode: {context.Response.StatusCode} \n  title: {exception.ProblemDetails.Title} \n details: {exception.ProblemDetails.Detail}");

                await context.Response.WriteAsync(JsonSerializer.Serialize(exception.ProblemDetails, _options));
            }
        }
    }
}