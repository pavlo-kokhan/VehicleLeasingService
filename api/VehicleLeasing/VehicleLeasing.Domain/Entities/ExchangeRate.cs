using System;
using System.Collections.Generic;

namespace VehicleLeasing.DataAccess.Entities;

public partial class ExchangeRate
{
    public int Id { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal Rate { get; set; }

    public DateOnly Date { get; set; }
}
