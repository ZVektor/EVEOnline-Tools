using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class TbUniverseStation
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int MaxDockableShipVolume { get; set; }

    public int OfficeRentalCost { get; set; }

    public int? Owner { get; set; }

    public decimal PositionX { get; set; }

    public decimal PositionY { get; set; }

    public decimal PositionZ { get; set; }

    public int? RaceId { get; set; }

    public decimal ReprocessingEfficiency { get; set; }

    public decimal ReprocessingStationsTake { get; set; }

    public string Services { get; set; } = null!;

    public int SystemId { get; set; }

    public int TypeId { get; set; }

    public virtual TbUniverseRace? Race { get; set; }

    public virtual TbUniverseSystem System { get; set; } = null!;

    public virtual ICollection<TbMarketOrder> TbMarketOrders { get; } = new List<TbMarketOrder>();

    public virtual TbUniverseType Type { get; set; } = null!;
}
