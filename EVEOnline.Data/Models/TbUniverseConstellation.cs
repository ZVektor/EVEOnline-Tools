using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class TbUniverseConstellation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal PositionX { get; set; }

    public decimal PositionY { get; set; }

    public decimal PositionZ { get; set; }

    public int RegionId { get; set; }

    public virtual TbUniverseRegion Region { get; set; } = null!;

    public virtual ICollection<TbUniverseSystem> TbUniverseSystems { get; } = new List<TbUniverseSystem>();
}
