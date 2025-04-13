using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class VehicleCategory
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
