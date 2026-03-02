using AgroSolutions.Application.Interfaces;
using AgroSolutions.Application.Services;
using AgroSolutions.Identity.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using AgroSolutions.Identity.Domain.Interfaces;
using AgroSolutions.Identity.Data.Repositories;
using AgroSolutions.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

//Configurations
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AgroSolutions Identity API",
        Version = "v1"
    });
});

string conStr = builder
    .Configuration
    .GetConnectionString("Auth") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContext<AuthContext>(opt =>
    opt.UseSqlServer(conStr));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.MapControllers();
app.MapHealthChecks("/healthz");


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuthContext>();
    db.Database.EnsureCreated();
    db.Database.Migrate();
}

app.Run();
