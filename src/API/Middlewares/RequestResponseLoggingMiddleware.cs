using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments(new PathString("/api")))
            {
                String requestBody;
                String responseBody = "";

                httpContext.Request.EnableBuffering();

                using (StreamReader reader = new StreamReader(httpContext.Request.Body,
                                                              encoding: Encoding.UTF8,
                                                              detectEncodingFromByteOrderMarks: false,
                                                              leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();

                    if (requestBody.Length > 0)
                        _logger.LogInformation($"Request Body: {requestBody}");

                    httpContext.Request.Body.Position = 0;
                }

                Stream originalResponseStream = httpContext.Response.Body;
                using (MemoryStream responseStream = new MemoryStream())
                {
                    httpContext.Response.Body = responseStream;

                    await _next(httpContext);

                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                    responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

                    if (responseBody.Length > 0)
                        _logger.LogInformation($"Response Body: {responseBody}");

                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

                    await responseStream.CopyToAsync(originalResponseStream);
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
