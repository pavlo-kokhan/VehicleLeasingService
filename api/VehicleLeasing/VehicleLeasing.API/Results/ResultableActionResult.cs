using Microsoft.AspNetCore.Mvc;
using IResult = VehicleLeasing.API.Results.Abstract.IResult;

namespace VehicleLeasing.API.Results;

public class ResultableActionResult : ActionResult
{
    public ResultableActionResult(IResult result)
    {
        Result = result;
    }

    public IResult Result { get; }
}