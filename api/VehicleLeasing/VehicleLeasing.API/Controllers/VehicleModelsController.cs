using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Queries;
using VehicleLeasing.API.Queries.Vehicles;

namespace VehicleLeasing.API.Controllers;

[ApiController]
[Route("vehicle-models")]
public class VehicleModelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleModelsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetByParametersAsync(string category, string brand, string contains, CancellationToken cancellationToken) 
        => (await _mediator.Send(new VehicleModelsQuery(category, brand, contains), cancellationToken)).ToActionResult();
}