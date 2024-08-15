using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Entity;
using Infra.Persistence;
using MediatR;
using System.Net.Http.Headers;

namespace Application.UserCQ.Handlers
{
    // classe que recebe requisicao CreateUserCommand e retorna um ResponseBase<UserInfoViewModel>
    public class CreateUserCommandHandler(TaskDbContext context, IMapper mapper) : IRequestHandler<CreateUserCommand, ResponseBase<UserInfoViewModel?>>
    {

        // injeções de dependencia
        private readonly TaskDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseBase<UserInfoViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // criando usuario tipo User recebendo os dados de request
            // neste caso estou usando o automaper
            var user = _mapper.Map<User>(request);

            // adicionando e salvando usuario
            _context.Users.Add(user);
            _context.SaveChanges();

            // depois de adicionar agora vamos criar o retorno
            var userInfo = new ResponseBase<UserInfoViewModel>()
            {
                ResponseInfo = null,
                Value = _mapper.Map<UserInfoViewModel>(user) // usando automapper criando um UserInfo
            };
            return userInfo;
        }
    }
}
