using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class LeasingRequest
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public Guid UserId { get; set; }

    public decimal FixedPrice { get; set; }

    public DateOnly Date { get; set; }

    public int StatusId { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual ICollection<LeasingRequestsModificationLog> LeasingRequestsModificationLogs { get; set; } = new List<LeasingRequestsModificationLog>();

    public virtual LeasingRequestStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
