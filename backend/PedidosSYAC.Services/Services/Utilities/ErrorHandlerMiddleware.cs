using Azure;
using Microsoft.AspNetCore.Http;
using Raven.Client.Exceptions;
using System;
using System.Text.Json;

namespace PedidosSYAC.Services.Services.Utilities
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = ex switch
                {
                    ConflictException => 409,
                    _ => 500
                };

                var resultado = JsonSerializer.Serialize(new { status = context.Response.StatusCode,error = ex.Message });
                await context.Response.WriteAsync(resultado);

            }
        }
    }
}
