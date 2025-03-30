using AuthHub.Application.Auth.v1.Commands;
using AuthHub.Application.Common;
using AuthHub.Application.Dtos.Response;
using AuthHub.Application.Services.v1.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthHub.Presentation.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Inicio de Sesión en la aplicación
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var login = await _mediator.Send(request);
            return Ok(new Response<LoginDto>()
            {
                StatusCode = 200,
                Message = "Login exitoso",
                Data = login
            });

        }


        /// <summary>
        /// Cierre de sesión en la aplicación
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut([FromBody] LogoutCommand request)
        {
            throw new NotImplementedException();
        }

        
    }
}

