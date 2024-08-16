using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Classe que contém os métodos action de entidade User.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        // injeções de dependencia
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Rota responsável pela criação de um usuário.
        /// </summary>
        /// <param name="comand">
        /// Um objeto CreateUserCommand
        /// </param>
        /// <returns>
        /// Os dados do usuário criado
        /// </returns>
        /// <remarks>
        /// Exemplo de request:
        /// ```
        /// POST /Auth/Create-User
        /// {
        ///     "name": "John",
        ///     "surname": "Doe",
        ///     "username": "JDoe",
        ///     "email": "jdoe@email.com",
        ///     "password": "123456"
        /// }
        /// ```
        /// </remarks>
        /// <response code="200">Retorna os dados de um novo usuário</response>
        /// <response code="400">Se algum dado for digitado incorretamente</response>
        [HttpPost("Create-User")]
        public async Task<ActionResult<UserInfoViewModel>> CreateUser(CreateUserCommand comand)
        {
            var result = await _mediator.Send(comand);

            return Ok(result);
        }
    }
}
