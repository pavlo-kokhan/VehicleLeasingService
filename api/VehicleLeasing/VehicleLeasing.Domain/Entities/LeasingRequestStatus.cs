using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class LeasingRequestStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<LeasingRequest> LeasingRequests { get; set; } = new List<LeasingRequest>();
}
