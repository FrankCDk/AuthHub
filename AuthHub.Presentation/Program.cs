using AuthHub.Application.Auth.v1.Commands;
using AuthHub.Application.Interfaces;
using AuthHub.Application.Validations.v1.Auth;
using AuthHub.Infrastructure.Auth;
using AuthHub.Infrastructure.Databases;
using AuthHub.Infrastructure.Dependency;
using AuthHub.Infrastructure.Middlewares;
using AuthHub.Infrastructure.Persistence.v1;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(_ => true);
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IDatabaseConnectionFactory, DatabaseFactory>();
builder.Services.AddScoped<IHttpContextService, HttpContextService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHistoryAccessRepository, HistoryAccessRepository>();
builder.Services.AddScoped<IGenerateToken, GenerateToken>();


builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
#region Versionamiento con API's

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Versionado por URL
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = $"Versioned API {description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        });
    }
});

#region Configuracion para MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
});
#endregion

builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<LoginComandValidation>(); //Registramos el fluen validation
builder.Services.AddValidatorsFromAssembly(typeof(LoginComandValidation).Assembly); //Registramos todas las validaciones del ensamblado donde este LoginCommand

//builder.Services.AddAutoMapper(typeof(LogginComandMapper)); //Registramos el mapper

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

builder.Services.AddApplicationServices();

#region Login con Gitlab
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = "GitLab";
//})
//.AddCookie()
//.AddOAuth("GitLab", options =>
//{
//    options.ClientId = builder.Configuration["GitLab:ClientId"];
//    options.ClientSecret = builder.Configuration["GitLab:ClientSecret"];
//    options.CallbackPath = "/api/v1/Login/login";

//    options.AuthorizationEndpoint = "https://gitlab.com/oauth/authorize";
//    options.TokenEndpoint = "https://gitlab.com/oauth/token";
//    options.UserInformationEndpoint = "https://gitlab.com/api/v4/user";

//    options.Scope.Add("read_user");
//    options.Scope.Add("openid");
//    options.Scope.Add("profile");
//    options.Scope.Add("email");

//    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
//    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
//    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

//    options.SaveTokens = true;

//    options.Events.OnCreatingTicket = async context =>
//    {
//        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
//        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);

//        var response = await context.Backchannel.SendAsync(request);
//        response.EnsureSuccessStatusCode();

//        var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
//        context.RunClaimActions(user.RootElement);
//    };
//});

//builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

//app.UseMiddleware<DatabaseTypeMiddleware>();
//app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<SwaggerAuthMiddleware>();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//En produccion no se debe subir de esta manera
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
