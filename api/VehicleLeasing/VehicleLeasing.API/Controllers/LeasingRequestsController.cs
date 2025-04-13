using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleLeasing.API.Commands;
using VehicleLeasing.API.Commands.LeasingRequests;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Contracts.QueryParameters.Common;
using VehicleLeasing.API.Contracts.QueryParameters.LeasingRequests;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Queries.LeasingRequests;

namespace VehicleLeasing.API.Controllers;

[ApiController]
[Route("leasing-requests")]
public class LeasingRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeasingRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("page")]
    [Authorize(Roles = $"{IdentityData.ManagerRoleName},{IdentityData.AdminRoleName}")]
    public async Task<IActionResult> GetPageAsync(
        [FromQuery] LeasingRequestsFilter? filter, 
        [FromQuery] SortParameters? sortParameters, 
        [FromQuery] PageParameters? pageParameters, 
        CancellationToken cancellationToken)
        => (await _mediator.Send(new LeasingRequestsPageQuery(filter, sortParameters, pageParameters), cancellationToken))
            .ToActionResult();
    
    [HttpGet]
    [Authorize(Roles = $"{IdentityData.ManagerRoleName},{IdentityData.AdminRoleName}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken) 
        => (await _mediator.Send(new LeasingRequestQuery(id), cancellationToken)).ToActionResult();
    
    [HttpPost]
    [Authorize(Roles = IdentityData.UserRoleName)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateLeasingRequestCommand request, CancellationToken cancellationToken) 
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPut("approve")]
    [Authorize(Roles = $"{IdentityData.ManagerRoleName},{IdentityData.AdminRoleName}")]
    public async Task<IActionResult> ApproveAsync([FromBody] ApproveLeasingRequestCommand request, CancellationToken cancellationToken) 
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPut("decline")]
    [Authorize(Roles = $"{IdentityData.ManagerRoleName},{IdentityData.AdminRoleName}")]
    public async Task<IActionResult> DeclineAsync([FromBody] DeclineLeasingRequestCommand request, CancellationToken cancellationToken) 
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpDelete]
    [Authorize(Roles = $"{IdentityData.ManagerRoleName},{IdentityData.AdminRoleName}")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteLeasingRequestCommand request, CancellationToken cancellationToken) 
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}