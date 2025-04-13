using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Abstractions.Auth;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.Users;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Commands.Users;

public record LoginUserCommand(
    string Email, 
    string Password)
    : IRequest<Result<UserTokenResponse>>
{
    public class Handler : IRequestHandler<LoginUserCommand, Result<UserTokenResponse>>
    {
        private readonly VehicleLeasingDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public Handler(VehicleLeasingDbContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<UserTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .Where(u => u.Email == request.Email)
                .Select(u => new UserResponse(
                    u.Id, 
                    u.Role.Name, 
                    u.Name, 
                    u.Surname, 
                    u.Email, 
                    u.PasswordHash))
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null) return AuthValidationErrors.InvalidEmail;
            
            var result = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!result) return AuthValidationErrors.InvalidPassword;

            return new UserTokenResponse(_jwtProvider.GenerateToken(user));
        }
    }
}