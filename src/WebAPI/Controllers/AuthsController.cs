using Application.Auth.Dtos;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            TokenDto result = await Mediator.Send(registerCommand);
            return Created("", result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            TokenDto result = await Mediator.Send(loginCommand);
            return Created("", result);
        }
    }
}