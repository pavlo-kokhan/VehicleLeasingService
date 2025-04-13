using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class Vehicle
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public decimal EstimatedPrice { get; set; }

    public int CategoryId { get; set; }

    public int TransmissionId { get; set; }

    public int FuelTypeId { get; set; }

    public int StatusId { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual VehicleCategory Category { get; set; } = null!;

    public virtual VehicleFuelType FuelType { get; set; } = null!;

    public virtual ICollection<LeasingRequest> LeasingRequests { get; set; } = new List<LeasingRequest>();

    public virtual VehicleStatus Status { get; set; } = null!;

    public virtual VehicleTransmission Transmission { get; set; } = null!;

    public virtual ICollection<VehiclesModificationLog> VehiclesModificationLogs { get; set; } = new List<VehiclesModificationLog>();
}
