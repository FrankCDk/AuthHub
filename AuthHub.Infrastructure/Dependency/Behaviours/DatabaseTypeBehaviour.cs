using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuthHub.Infrastructure.Dependency.Behaviours
{
    public class DatabaseTypeBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatabaseTypeBehaviour(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("No HttpContext available");

            if (context.Request.Path.Value.Contains("swagger"))
            {
                return await next();
            }

            // Verifica si el encabezado "Database-Type" está presente
            if (!context.Request.Headers.TryGetValue("Database-Type", out var databaseType))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                throw new InvalidOperationException("El encabezado 'Database-Type' es requerido.");
            }

            // Agrega el tipo de base de datos como un valor en Items del HttpContext
            context.Items["DatabaseType"] = databaseType.ToString();

            // Continúa con el siguiente comportamiento del pipeline
            return await next();
        }
    }
}
