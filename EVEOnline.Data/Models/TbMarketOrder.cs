using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class TbMarketOrder
{
    public long Id { get; set; }

    public int Duration { get; set; }

    public bool IsBuyOrder { get; set; }

    public DateTime Issued { get; set; }

    public long LocationId { get; set; }

    public int MinVolume { get; set; }

    public decimal Price { get; set; }

    public string Range { get; set; } = null!;

    public int SystemId { get; set; }

    public int TypeId { get; set; }

    public int VolumeRemain { get; set; }

    public int VolumeTotal { get; set; }

    public virtual TbUniverseStation Location { get; set; } = null!;

    public virtual TbUniverseSystem System { get; set; } = null!;

    public virtual TbUniverseType Type { get; set; } = null!;
}
