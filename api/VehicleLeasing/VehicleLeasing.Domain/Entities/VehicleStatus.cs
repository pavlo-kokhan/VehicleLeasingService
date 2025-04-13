using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class VehicleStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
