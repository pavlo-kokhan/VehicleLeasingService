using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Queries.Vehicles.Validations;

namespace VehicleLeasing.API.Controllers;

[ApiController]
[Route("vehicle-years")]
public class VehicleYearsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleYearsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string category, string brand, string model, string contains,CancellationToken cancellationToken) 
        => (await _mediator.Send(new VehicleYearsQuery(category, brand, model, contains), cancellationToken)).ToActionResult();
}