using AuthHub.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthHub.Infrastructure.Middlewares
{
    public class HttpContextService : IHttpContextService
    {

        private readonly IHttpContextAccessor _contextAccessor; // Nos permite acceder a los encabezados HTTP

        public HttpContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? GetHeaderValue(string headerName)
        {
            if (_contextAccessor.HttpContext == null)
            {
                throw new InvalidOperationException("No se puede acceder al HttpContext.");
            }

            if (_contextAccessor.HttpContext.Request.Headers.TryGetValue(headerName, out var headerValue))
            {
                return headerValue;
            }

            throw new KeyNotFoundException($"El encabezado '{headerName}' no está presente en la solicitud.");
        }
    }
}
