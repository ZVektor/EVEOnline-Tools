using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class TbUniverseRace
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NameRu { get; set; }

    public string Description { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public int AllianceId { get; set; }

    public virtual ICollection<TbUniverseStation> TbUniverseStations { get; } = new List<TbUniverseStation>();
}
