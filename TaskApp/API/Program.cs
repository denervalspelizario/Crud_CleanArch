using Application.UserCQ.Commands;
using Infra.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config Banco de dados
var configurationDb = builder.Configuration;
builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(configurationDb.GetConnectionString("DefaultConnection")));


/* Config Mediator referÍnciando 1 (CreateUserComand) 
   ja referÍncia todos os outros que estiverem no mesmo assembly*/
builder.Services.AddMediatR(
    config => config.RegisterServicesFromAssemblies(
        typeof(CreateUserCommand).Assembly));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
