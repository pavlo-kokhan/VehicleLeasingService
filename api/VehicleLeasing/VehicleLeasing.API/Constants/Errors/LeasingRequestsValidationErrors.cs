using VehicleLeasing.API.Contracts.Validation;

namespace VehicleLeasing.API.Constants.Errors;

public static class LeasingRequestsValidationErrors
{
    public static readonly ValidationError LeasingRequestNotFound = ValidationError.CreateWithMessage("LEASING_REQUEST_NOT_FOUND", "Leasing request is not found");
    
    public static readonly ValidationError LeasingRequestStatusNotFound = ValidationError.CreateWithMessage("LEASING_REQUEST_STATUS_NOT_FOUND", "Leasing request status is not found");

}