using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VehicleLeasing.API.Contracts.Jwt;

namespace VehicleLeasing.API.Extensions;

public static class ServiceCollectionAuthenticationExtensions
{
    public static void AddApiAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtOptions>>();
                
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                };
            });

        services.AddAuthorization();
    }
}