using Application.UserCQ.Commands;
using Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Application.UserCQ.Validators;
using FluentValidation.AspNetCore;
using Application.Mappings;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Abstractions;
using Services.AuthService;


namespace API
{
    // Classe que será usada para organizar o program.cs quando o projeto esta muito grande
    public static class BuilderExtensions
    {

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            /* Config Mediator referênciando 1 (CreateUserComand) 
               ja referência todos os outros que estiverem no mesmo assembly*/
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                    typeof(CreateUserCommand).Assembly));
        }




        // metodo para customizar o swagger
        public static void AddSwaggerDocs(this WebApplicationBuilder builder)
        {
            
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Task App",
                    Description = "Um aplicativo de tarefas baseado no Trello e escrito em ASP.NET Core V8",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact 
                    {
                        Name = "Exemplo de página de contato",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense 
                    {
                        Name = "Exemplo de página de licença",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });
        }


        // método de autenticação
        public static void AddJwtAuth(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                };
            });
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

        // método do automapper
        public static void AddMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(ProfileMappings).Assembly);
        }


        // metodo das injeções de dependencias
        public static void AddInjections(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthService>();
        }
    }
}
