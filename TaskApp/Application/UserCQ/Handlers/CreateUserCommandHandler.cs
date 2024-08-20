using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entity;
using Infra.Persistence;
using MediatR;
using System.Net.Http.Headers;

namespace Application.UserCQ.Handlers
{
    // classe que recebe requisicao CreateUserCommand e retorna um ResponseBase<UserInfoViewModel>
    public class CreateUserCommandHandler(
        TaskDbContext context, 
        IMapper mapper, 
        IAuthService authService) : IRequestHandler<CreateUserCommand, ResponseBase<UserInfoViewModel?>>
    {

        // injeções de dependencia
        private readonly TaskDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthService _authService = authService;

        public async Task<ResponseBase<UserInfoViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // criando usuario tipo User recebendo os dados de request
            // neste caso estou usando o automaper
            var user = _mapper.Map<User>(request);

            // usando o metodo Generate e adicionando novo token no campo RefreshToken 
            user.RefresToken = _authService.GenerateRefreshToken();

            // usando o método HashingPassword para gerar a senha hash
            user.Password = _authService.HashingPassword(request.Password!);


            // adicionando e salvando usuario
            _context.Users.Add(user);
            _context.SaveChanges();

            // usando automapper criando um UserInfo e adicionando na variavel userInfoVm
            var userInfoVm = _mapper.Map<UserInfoViewModel>(user);

            // usando o metodo GenerateJWT criação o refreshtoken e passa para o campo RefreshToken
            userInfoVm.TokenJWT = _authService.GenerateJWT(user.Email!, user.Username!);


            // depois de adicionar agora vamos criar o retorno
            var userInfo = new ResponseBase<UserInfoViewModel>()
            {
                ResponseInfo = null,
                Value = userInfoVm
            };
            return userInfo;
        }
    }
}
