using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleLeasing.API.Commands;
using VehicleLeasing.API.Commands.Users;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Extensions;

namespace VehicleLeasing.API.Controllers;

[ApiController]
[Route("auth")]  
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand request, CancellationToken cancellationToken) 
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand request, CancellationToken cancellationToken) 
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();

    [HttpPost("generate")]
    [Authorize(Roles = $"{IdentityData.ManagerRoleName},{IdentityData.AdminRoleName}")]
    public async Task<IActionResult> GenerateAsync([FromBody] GenerateUsersCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}