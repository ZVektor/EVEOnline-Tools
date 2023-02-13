using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class TbUniverseTypeB
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double? Capacity { get; set; }

    public string Description { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public string? DogmaAttributes { get; set; }

    public string? DogmaEffects { get; set; }

    public int? GraphicId { get; set; }

    public int GroupId { get; set; }

    public int? IconId { get; set; }

    public int? MarketGroupId { get; set; }

    public double? Mass { get; set; }

    public double? PackagedVolume { get; set; }

    public int? PortionSize { get; set; }

    public bool Published { get; set; }

    public double? Radius { get; set; }

    public double? Volume { get; set; }
}
