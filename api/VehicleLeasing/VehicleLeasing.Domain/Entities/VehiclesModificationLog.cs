using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class VehiclesModificationLog
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public string Operation { get; set; } = null!;

    public DateTime Time { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
