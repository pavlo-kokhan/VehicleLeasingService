using Microsoft.AspNetCore.Mvc;
using VehicleLeasing.API.Results;
using IResult = VehicleLeasing.API.Results.Abstract.IResult;

namespace VehicleLeasing.API.Extensions;

public static class ActionResultExtensions
{
    public static IActionResult ToActionResult(this IResult result)
        => new ResultableActionResult(result);
}