using Bogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Abstractions.Auth;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results;
using VehicleLeasing.DataAccess.DbContexts;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.API.Commands.Users;

public record GenerateUsersCommand(int Count) : IRequest<Result>
{
    public class Handler : IRequestHandler<GenerateUsersCommand, Result>
    {
        private readonly VehicleLeasingDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(VehicleLeasingDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> Handle(GenerateUsersCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker();
        
            var passwordHash = _passwordHasher.Generate("1234");

            for (int i = 0; i < request.Count; i++)
            {
                var name = faker.Name.FirstName();
                var surname = faker.Name.LastName();
                var email = $"{name.ToLower()}{surname.ToLower()}@gmail.com";
                
                var requestEmailUser = await _context.Users
                    .AsNoTracking()
                    .Where(u => u.Email == email)
                    .FirstOrDefaultAsync(cancellationToken);

                if (requestEmailUser is not null) 
                    return AuthValidationErrors.EmailAlreadyExists;
            
                var userEntity = new User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Surname = surname,
                    Email = email,
                    PasswordHash = passwordHash,
                    RoleId = 1
                };
            
                await _context.AddAsync(userEntity, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}