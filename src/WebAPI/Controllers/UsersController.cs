using Application.Features.Users.Commands;
using Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
        {
            UpdatedUserDto updatedUserDto = await Mediator.Send(updateUserCommand);

            return Ok(updatedUserDto);
        }
    }
}
