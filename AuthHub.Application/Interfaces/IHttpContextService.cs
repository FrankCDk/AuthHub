using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthHub.Application.Interfaces
{
    public interface IHttpContextService
    {
        string? GetHeaderValue(string headerName); //Obtenemos el valor de la cabecera
    }
}
