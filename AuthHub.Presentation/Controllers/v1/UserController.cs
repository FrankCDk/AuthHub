using AuthHub.Application.Common;
using AuthHub.Application.Dtos.Response;
using AuthHub.Application.Services.v1.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthHub.Presentation.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request)
        {
            await _mediator.Send(request);
            return Ok(new Response()
            {
                StatusCode = 201,
                Message = "Usuario registrado exitosamente.",
            });
        }
    }
}
