using System.Reflection;
using DotNetEnv;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Abstractions.Auth;
using VehicleLeasing.API.Abstractions.Services;
using VehicleLeasing.API.BackgroundServices;
using VehicleLeasing.API.Contracts.ExchangeRates;
using VehicleLeasing.API.Contracts.Jwt;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Filters;
using VehicleLeasing.API.Services;
using VehicleLeasing.DataAccess.DbContexts;

Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

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
services.AddSwaggerGen();

services.AddDbContext<VehicleLeasingDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
});

services.AddScoped<IPasswordHasher, EnhancedPasswordHasher>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IVehicleMonthlyPaymentCalculator, VehicleMonthlyPaymentCalculator>();

services.AddHttpClient<IExchangeRateService, ExchangeRateService>();

// services.AddHostedService<ExchangeRatesUpdaterBackgroundService>();

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
await scope.ServiceProvider.GetRequiredService<VehicleLeasingDbContext>().Database.MigrateAsync();

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
