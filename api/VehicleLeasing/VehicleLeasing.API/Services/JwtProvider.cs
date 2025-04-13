using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VehicleLeasing.API.Abstractions.Auth;
using VehicleLeasing.API.Contracts.Jwt;
using VehicleLeasing.API.Contracts.Users;

namespace VehicleLeasing.API.Services;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;
    
    public string GenerateToken(UserResponse userResponse)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, userResponse.Id.ToString()),
            new Claim(ClaimTypes.Role, userResponse.Role)];

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SecretKey));
        
        var signingCredentials = new SigningCredentials(
            securityKey, 
            SecurityAlgorithms.HmacSha256Signature);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials, 
            expires: DateTime.Now.AddHours(_options.ExpiresHours));
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}