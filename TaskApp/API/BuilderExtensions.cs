using Application.UserCQ.Commands;
using Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Application.UserCQ.Validators;
using FluentValidation.AspNetCore;

namespace API
{
    // Classe que será usada para organizar o program.cs quando o projeto esta muito grande
    public static class BuilderExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            /* Config Mediator referênciando 1 (CreateUserComand) 
               ja referência todos os outros que estiverem no mesmo assembly*/
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                    typeof(CreateUserCommand).Assembly));
        }


        // metodo para uso do contexto do database
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            // Config Banco de dados
            var configurationDb = builder.Configuration;
            builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(configurationDb.GetConnectionString("DefaultConnection")));
        }


        // metodos dos servicos de validação
        public static void AddValidations(this WebApplicationBuilder builder)
        {
            // config FluentValidation(obs precisa referenciar apenas 1 classe que ele entende onde
            // fica as classes de validação)
            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserComandValidator>();
            builder.Services.AddFluentValidationAutoValidation();

        }
    }
}
