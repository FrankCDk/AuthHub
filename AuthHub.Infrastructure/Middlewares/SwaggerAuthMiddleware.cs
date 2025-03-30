using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace AuthHub.Infrastructure.Middlewares
{
    public class SwaggerAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string? _username;
        private readonly string? _password;

        public SwaggerAuthMiddleware(RequestDelegate next, IConfiguration configuration )
        {
            _next = next;
            _username = configuration["SwaggerAuth:Username"] ?? "admin";
            _password = configuration["SwaggerAuth:Password"] ?? "1234";
        }

        public async Task InvokeAsync(HttpContext context) // Context : tiene información de la solicitud o respuesta.
        {

            if (context.Request.Path.StartsWithSegments("/swagger")) // Validamos si se esta entrando al swagger
            {

                var authHeader = context.Request.Headers["Authorization"].ToString(); // Obtenemos los datos de la cabecera authenticacion
                if(authHeader != null && authHeader.StartsWith("Basic "))
                {
                    var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    var username = decodedUsernamePassword.Split(':')[0];
                    var password = decodedUsernamePassword.Split(':')[1];

                    if(username == _username &&  password == _password)
                    {
                        await _next(context);
                        return;
                    }
                }

                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");

            }
            else
            {
                await _next(context); //Continuamos con el proceso
            }



        }


    }
}
