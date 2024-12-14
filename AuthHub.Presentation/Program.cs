using AuthHub.Application.Dto.Mappers;
using AuthHub.Application.Interfaces;
using AuthHub.Application.Services;
using AuthHub.Domain.Interfaces;
using AuthHub.Infrastructure.Connections;
using AuthHub.Infrastructure.Interfaces;
using AuthHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Register services
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IDatabaseConnection, MySQLDatabase>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");
    return new MySQLDatabase(connectionString);
});

//Register repositories Domain
builder.Services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
