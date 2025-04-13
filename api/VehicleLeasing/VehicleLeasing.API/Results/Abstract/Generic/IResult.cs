namespace VehicleLeasing.API.Results.Abstract.Generic;

public interface IResult<out T> : IResult
{
    T Data { get; }
}