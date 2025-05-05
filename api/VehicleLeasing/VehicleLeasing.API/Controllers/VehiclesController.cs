using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleLeasing.API.Commands.Vehicles;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Contracts.QueryParameters.Common;
using VehicleLeasing.API.Contracts.QueryParameters.Vehicles;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Queries.Vehicles;

namespace VehicleLeasing.API.Controllers;

[ApiController]
[Route("vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehiclesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("page")]
    [Authorize(Roles = UserRoleNames.Administrator)]
    public async Task<IActionResult> GetPageAsync(
        [FromQuery] VehiclesFilter? filter,
        [FromQuery] SortParameters? sortParameters,
        [FromQuery] PageParameters? pageParameters,
        CancellationToken cancellationToken)
        => (await _mediator.Send(new VehiclesPageQuery(filter, sortParameters, pageParameters), cancellationToken))
            .ToActionResult();
    
    [HttpGet]
    [Authorize(Roles = UserRoleNames.Administrator)]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        => (await _mediator.Send(new VehicleQuery(id), cancellationToken)).ToActionResult();
    
    [HttpPost]
    [Authorize(Roles = UserRoleNames.Administrator)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateVehicleCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPut]
    [Authorize(Roles = UserRoleNames.Administrator)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateVehicleRequest request, CancellationToken cancellationToken)
        => (await _mediator.Send(new UpdateVehicleCommand(
            id, 
            request.Brand, 
            request.Model, 
            request.Year, 
            request.EstimatedPrice, 
            request.Category, 
            request.Transmission, 
            request.FuelType, 
            request.Status), cancellationToken)).ToActionResult();
    
    [HttpDelete]
    [Authorize(Roles = UserRoleNames.Administrator)]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        => (await _mediator.Send(new DeleteVehicleCommand(id), cancellationToken)).ToActionResult();
    
    [HttpGet("leasing-calculator")]
    public async Task<IActionResult> GetByParametersAsync(
        string category,
        string brand, 
        string model,
        int year,
        int leasingMonths,
        int advancePercentage,
        CancellationToken cancellationToken)
        => (await _mediator.Send(new VehicleLeasingCalculatorQuery(
                    category, 
                    brand, 
                    model, 
                    year,
                    leasingMonths, 
                    advancePercentage), 
                cancellationToken))
            .ToActionResult();
}