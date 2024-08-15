using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        // injeções de dependencia
        private readonly IMediator _mediator = mediator;

        // Criação de usuário
        [HttpPost("Create-User")]
        public async Task<ActionResult<UserInfoViewModel>> CreateUser(CreateUserCommand comand)
        {
            var result = await _mediator.Send(comand);

            return Ok(result);
        }
    }
}
