using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class Usystem
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

    public virtual Uconstellation Constellation { get; set; } = null!;
}
