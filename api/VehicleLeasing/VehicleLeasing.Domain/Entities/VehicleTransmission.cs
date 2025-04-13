using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class VehicleTransmission
{
    public int Id { get; set; }

    public string Transmission { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
