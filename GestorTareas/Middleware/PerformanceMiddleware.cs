using System.Diagnostics;

namespace GestorTareas.Middleware
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMiddleware> _logger;

        public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                // Añadir header ANTES de procesar la request
                context.Response.OnStarting(() =>
                {
                    if (!context.Response.Headers.ContainsKey("X-Response-Time-ms"))
                    {
                        context.Response.Headers.Append("X-Response-Time-ms", stopwatch.ElapsedMilliseconds.ToString());
                    }
                    return Task.CompletedTask;
                });

                await _next(context);
            }
            finally
            {
                stopwatch.Stop();
                
                var elapsedMs = stopwatch.ElapsedMilliseconds;
                var path = context.Request.Path;
                var method = context.Request.Method;
                var statusCode = context.Response.StatusCode;

                if (elapsedMs > 1000) // Log si toma más de 1 segundo
                {
                    _logger.LogWarning(
                        "Slow request: {Method} {Path} took {ElapsedMs}ms - Status: {StatusCode}",
                        method, path, elapsedMs, statusCode);
                }
                else if (elapsedMs > 500) // Log si toma más de 500ms
                {
                    _logger.LogInformation(
                        "Request: {Method} {Path} took {ElapsedMs}ms - Status: {StatusCode}",
                        method, path, elapsedMs, statusCode);
                }

                // NO añadir headers aquí - la respuesta ya se envió
            }
        }
    }
}