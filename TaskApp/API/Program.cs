using API;

var builder = WebApplication.CreateBuilder(args);

// AddServices e AddDatabase usado para melhor organização do projeto
builder.AddServices();
builder.AddDatabase();
builder.AddValidations();
builder.AddMapper();
builder.AddSwaggerDocs();
builder.AddJwtAuth();
builder.AddInjections();


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
