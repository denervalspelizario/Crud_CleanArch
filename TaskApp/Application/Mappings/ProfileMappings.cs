using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Entity;

namespace Application.Mappings
{
    public class ProfileMappings : Profile
    {
        public ProfileMappings()
        {
            // usando o create estou passando os dados do createuser para user
            // Obs no user tem o campo refrestoken e RefreshTokenExpirationTime
            // então estou passando manualmente os valores dos campos
            CreateMap<CreateUserCommand, User>()
                .ForMember(x => x.RefresToken, x => x.MapFrom(x => GenerateGuid()))
                .ForMember(x => x.RefreshTokenExpirationTime, x => x.MapFrom(x => AddFiveDays()))
                .ForMember(x => x.Password, x => x.MapFrom(x => x.Password));


            // usando o createmap estou passando os dados do user para userinfo
            // mesmo caso de cima em UserInfo existe campo token que não existe em User 
            // por isso estou passando manualmente usando o metodo generateguid
            CreateMap<User, UserInfoViewModel>()
                .ForMember(x => x.TokenJWT, x => x.MapFrom(x => GenerateGuid()));

        }



        // metodo temporario que gera um Guid aleatorio
        private string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        // método que gera uma data 5 dias depois de hoje
        private DateTime AddFiveDays()
        {
            return DateTime.Now.AddDays(5);
        }
    }
}
