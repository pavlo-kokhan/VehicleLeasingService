﻿using System.Collections;
using VehicleLeasing.API.Contracts.Validation;
using VehicleLeasing.API.Results.Abstract.Generic;

namespace VehicleLeasing.API.Results.Generic;

public class Result<TData> : Result, IResult<TData>
{
    private Result(TData data)
        => Data = data;

    private Result(IDictionary<string, ValidationError> errors, ResultStatus resultStatus, Exception? exception)
        : base(errors, resultStatus, exception)
    {
    }

    private Result(ValidationError error)
        : base(error)
    {
    }

    protected Result(IDictionary<string, string?> errors, Exception? exception, ResultStatus resultStatus)
        : base(errors, exception, resultStatus)
    {
    }

    protected Result(string errorCode, Exception? exception, ResultStatus resultStatus)
        : base(errorCode, exception, resultStatus)
    {
    }

    public TData Data { get; } = default!; // Need to check if result is success before using this property.

    public static implicit operator Result<TData>(TData data)
        => new(data);

    public static implicit operator Result<TData>(ValidationError validationError)
        => new(validationError);

    public static Result<TData> Failure(IDictionary<string, string?> errors, ResultStatus resultStatus = ResultStatus.InvalidArgument)
        => new(errors, null, resultStatus);
    
    public static Result<TData> Failure(ValidationError validationError)
        => new(validationError);
    
    public new static Result<TData> Failure(IDictionary<string, ValidationError> errors, Exception? exception, ResultStatus resultStatus)
        => new(errors, resultStatus, exception);

    public static Result<TData> Success(TData data)
        => new(data);

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}