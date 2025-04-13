using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Abstractions.Auth;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.Users;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.API.Commands.Users;

public record RegisterUserCommand(
    string Name, 
    string Surname, 
    string Email, 
    string Password)
    : IRequest<Result<UserTokenResponse>>
{
    public class Handler : IRequestHandler<RegisterUserCommand, Result<UserTokenResponse>>
    {
        private readonly VehicleLeasingDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public Handler(VehicleLeasingDbContext context,IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<UserTokenResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var requestEmailUser = await _context.Users
                .AsNoTracking()
                .Where(u => u.Email == request.Email)
                .FirstOrDefaultAsync(cancellationToken);

            if (requestEmailUser is not null) 
                return AuthValidationErrors.EmailAlreadyExists;
            
            var hashedPassword = _passwordHasher.Generate(request.Password);
        
            var user = new UserResponse(
                Guid.NewGuid(),
                IdentityData.UserRoleName, 
                request.Name, 
                request.Surname, 
                request.Email, 
                hashedPassword);

            var userEntity = new User
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                RoleId = 1
            };
            
            await _context.Users.AddAsync(userEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new UserTokenResponse(_jwtProvider.GenerateToken(user));
        }
    }
}
