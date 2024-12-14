using AuthHub.Application.Dto.Login;
using AuthHub.Application.Dto.Register;
using AuthHub.Application.Dto.Token;
using AuthHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthHub.Presentation.Controllers
{

    [ApiController]
    [Route("v1/login/jwt/[action]")]
    public class AuthJWTController : Controller
    {


        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;

        public AuthJWTController(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        /// <summary>
        /// Endpoint to login a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            LoginResponse response = await _loginService.Login(request);
            return Ok();

        }



        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest("El formato de los datos enviados en el registro es invalido.");
            }

            try
            {
                
                if(await _registerService.RegisterAsync(request))
                {
                    return Ok("Registro exitoso.");
                }
                else { return BadRequest("Error al registrar el usuario."); }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            return View();
        }



    }
}
