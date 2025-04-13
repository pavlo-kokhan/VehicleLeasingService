using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class LeasingRequestsModificationLog
{
    public int Id { get; set; }

    public int LeasingRequestId { get; set; }

    public string Operation { get; set; } = null!;

    public DateTime Time { get; set; }

    public virtual LeasingRequest LeasingRequest { get; set; } = null!;
}
