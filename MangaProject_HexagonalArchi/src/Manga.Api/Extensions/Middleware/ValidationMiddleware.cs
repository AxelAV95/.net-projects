using MangaProject.Application.UseCase.Commons.Bases;
using MangaProject.Application.UseCase.Commons.Exceptions;
using System.Text.Json;

namespace MangaProject.Api.Extensions.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invole(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }catch(ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object>
                {
                    Message = "Validation Errors",
                    Errors = ex.Errors
                }) ;
            }
        }
    }
}
