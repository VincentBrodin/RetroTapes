using System;
using System.Collections.Generic;

namespace RetroTapes.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int CustomerId { get; set; }

    public byte StaffId { get; set; }

    public int? RentalId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Rental? Rental { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
