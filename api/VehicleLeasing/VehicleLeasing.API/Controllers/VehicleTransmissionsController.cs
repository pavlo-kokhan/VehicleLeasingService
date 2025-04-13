using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Queries;
using VehicleLeasing.API.Queries.Vehicles;

namespace VehicleLeasing.API.Controllers;

[ApiController]
[Route("vehicle-transmissions")]
public class VehicleTransmissionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleTransmissionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken) 
        => (await _mediator.Send(new VehicleTransmissionsQuery(), cancellationToken)).ToActionResult();
    
    [HttpGet]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken) 
        => (await _mediator.Send(new VehicleTransmissionQuery(id), cancellationToken)).ToActionResult();
}