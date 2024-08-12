using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using Domain.Entity;
using Infra.Persistence;
using MediatR;

namespace Application.UserCQ.Handlers
{
    // classe que recebe requisicao CreateUserCommand e retorna um UserInfoViewModel
    public class CreateUserCommandHandler(TaskDbContext context) : IRequestHandler<CreateUserCommand, UserInfoViewModel>
    {

        // injeções de dependencia
        private readonly TaskDbContext _context = context;

        public async Task<UserInfoViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        { 
            // criando usuario tipo User recebendo os dados de request
            var user = new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password,
                Username = request.Username,
                RefresToken = Guid.NewGuid().ToString(),
                RefreshTokenExpirationTime = DateTime.Now.AddDays(5)
            };

            // adicionando e salvando usuario
            _context.Users.Add(user);
            _context.SaveChanges();

            // depois de adicionar agora vamos criar o retorno
            var userInfo = new UserInfoViewModel()
            {
                Name = user.Name,
                SurName = user.Surname,
                Email = user.Email,
                Username = user.Username,
                RefresToken = user.RefresToken,
                RefreshTokenExpirationTime = user.RefreshTokenExpirationTime,
                TokenJWT =  Guid.NewGuid().ToString(), // como ainda não tem gerenado um aleatorio
            };
            return userInfo;
        }
    }
}
