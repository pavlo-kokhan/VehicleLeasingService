using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Queries;
using VehicleLeasing.API.Queries.Vehicles;

namespace VehicleLeasing.API.Controllers;

[ApiController]
[Route("vehicle-brands")]
public class VehicleBrandsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleBrandsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetByParametersAsync(string category, string contains, CancellationToken cancellationToken) 
        => (await _mediator.Send(new VehicleBrandsQuery(category, contains), cancellationToken)).ToActionResult();
}