﻿using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class Permission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
