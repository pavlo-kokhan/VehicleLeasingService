using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class LeasingInterestRate
{
    public int Id { get; set; }

    public int MinAdvancePercent { get; set; }

    public int MaxAdvancePercent { get; set; }

    public int MinMonths { get; set; }

    public int MaxMonths { get; set; }

    public decimal InterestRate { get; set; }
}
