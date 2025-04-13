using VehicleLeasing.API.Abstractions.Auth;

namespace VehicleLeasing.API.Services;

public class EnhancedPasswordHasher : IPasswordHasher
{
    public string Generate(string password) => 
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string hashedPassword) => 
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}