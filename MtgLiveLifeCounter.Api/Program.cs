using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MtgLiveLifeCounter.Api;
using MtgLiveLifeCounter.Api.Facades;
using MtgLiveLifeCounter.Api.Hubs;
using MtgLiveLifeCounter.Business.CommandHandlers;
using MtgLiveLifeCounter.Core.Configurations;
using MtgLiveLifeCounter.Core.Contracts;
using MtgLiveLifeCounter.Domain.Contracts.Repositories;
using MtgLiveLifeCounter.Infra.Connections;
using MtgLiveLifeCounter.Infra.Repositories;
using MtgLiveLifeCounter.Infra.UnitOfWork;
using MtgLiveLifeCounter.Swagger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var services = builder.Services;

services.AddDbContext<MtgLiveLifeCounterDbContext>(opts => opts
    .UseSqlServer(configuration.GetConnectionString("MtgLiveLifeCounter")).UseLazyLoadingProxies()); ;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IUow, Uow>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRoomRepository, RoomRepository>();

var authenticationSection = configuration
    .GetSection("Authentication");

services.Configure<AuthenticationConfiguration>(authenticationSection);

var authenticationConfiguration = authenticationSection
    .Get<AuthenticationConfiguration>();

services.AddAuthorization();
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var utf8Key = Encoding.UTF8.GetBytes(authenticationConfiguration.IssuerSigningKey);

    options.SaveToken = authenticationConfiguration.SaveToken;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = authenticationConfiguration.ValidateIssuer,
        ValidateAudience = authenticationConfiguration.ValidateAudience,
        ValidateLifetime = authenticationConfiguration.ValidateLifetime,
        ValidateIssuerSigningKey = authenticationConfiguration.ValidateIssuerSiginingKey,
        ValidIssuer = authenticationConfiguration.ValidIssuer,
        ValidAudience = authenticationConfiguration.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(utf8Key),
    };
});

services.AddApi(options => options.WithCQRSFromAssemblyOf<UserCommandHandler>());

services.AddSignalR();
services.AddScoped<ISignalRFacade, SignalRFacade>();
services.AddSingleton<SignalRHub>();
services.AddSwaggerGen((c) =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    c.OperationFilter<AuthorizeCheckOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<SignalRHub>("/signalr");

app.Run();
