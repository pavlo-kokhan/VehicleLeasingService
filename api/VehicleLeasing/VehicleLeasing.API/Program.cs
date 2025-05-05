using System.Reflection;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VehicleLeasing.API.Abstractions.Auth;
using VehicleLeasing.API.Abstractions.Services;
using VehicleLeasing.API.Contracts.ExchangeRates;
using VehicleLeasing.API.Contracts.Jwt;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Filters;
using VehicleLeasing.API.Services;
using VehicleLeasing.DataAccess.DbContexts;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<ExchangeRatesOptions>(configuration.GetSection(nameof(ExchangeRatesOptions)));

services.AddApiAuthentication();
services.AddControllers(c => c.Filters.Add<ResultableActionFilterAttribute>());
services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    c.Lifetime = ServiceLifetime.Scoped;
});
services.AddPipelines();
services.AddFluentValidation();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введіть JWT токен у форматі: Bearer {токен}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            []
        }
    });
});

services.AddDbContext<VehicleLeasingDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

services.AddScoped<IPasswordHasher, EnhancedPasswordHasher>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IVehicleMonthlyPaymentCalculator, VehicleMonthlyPaymentCalculator>();

services.AddHttpClient<IExchangeRateService, ExchangeRateService>();

var app = builder.Build();

app.UseCors(x =>
    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
