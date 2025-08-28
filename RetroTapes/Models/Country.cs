using System;
using System.Collections.Generic;

namespace RetroTapes.Models;

public partial class Country
{
    public short CountryId { get; set; }

    public string Country1 { get; set; } = null!;

    public DateTime? LastUpdate { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
