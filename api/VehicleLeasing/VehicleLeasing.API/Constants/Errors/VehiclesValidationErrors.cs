using VehicleLeasing.API.Contracts.Validation;

namespace VehicleLeasing.API.Constants.Errors;

public static class VehiclesValidationErrors
{
    public static readonly ValidationError VehicleNotFound = ValidationError.CreateWithMessage("VEHICLE_NOT_FOUND", "Vehicle is not found");
    
    public static readonly ValidationError VehicleCategoryNotFound = ValidationError.CreateWithMessage("VEHICLE_CATEGORY_NOT_FOUND", "Vehicle category is not found");
    
    public static readonly ValidationError VehicleFuelTypeNotFound = ValidationError.CreateWithMessage("VEHICLE_FUEL_TYPE_NOT_FOUND", "Vehicle fuel type is not found");
    
    public static readonly ValidationError VehicleTransmissionNotFound = ValidationError.CreateWithMessage("VEHICLE_TRANSMISSION_NOT_FOUND", "Vehicle transmission is not found");
    
    public static readonly ValidationError VehicleStatusNotFound = ValidationError.CreateWithMessage("VEHICLE_STATUS_NOT_FOUND", "Vehicle status is not found");
}