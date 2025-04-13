using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class VehicleFuelType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
