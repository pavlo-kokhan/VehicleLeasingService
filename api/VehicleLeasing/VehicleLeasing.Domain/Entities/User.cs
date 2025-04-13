using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class User
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public Guid Id { get; set; }

    public virtual ICollection<LeasingRequest> LeasingRequests { get; set; } = new List<LeasingRequest>();

    public virtual Role Role { get; set; } = null!;
}
