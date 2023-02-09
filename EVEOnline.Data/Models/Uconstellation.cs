using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class Uconstellation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal PositionX { get; set; }

    public decimal PositionY { get; set; }

    public decimal PositionZ { get; set; }

    public int RegionId { get; set; }

    public virtual Uregion Region { get; set; } = null!;

    public virtual ICollection<Usystem> Usystems { get; } = new List<Usystem>();
}
