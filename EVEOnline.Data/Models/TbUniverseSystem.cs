using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class TbUniverseSystem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal PositionX { get; set; }

    public decimal PositionY { get; set; }

    public decimal PositionZ { get; set; }

    public string? SecurityClass { get; set; }

    public decimal SecurityStatus { get; set; }

    public int? StarId { get; set; }

    public int ConstellationId { get; set; }

    public virtual TbUniverseConstellation Constellation { get; set; } = null!;

    public virtual ICollection<TbMarketOrder> TbMarketOrders { get; } = new List<TbMarketOrder>();

    public virtual ICollection<TbUniverseStation> TbUniverseStations { get; } = new List<TbUniverseStation>();
}
