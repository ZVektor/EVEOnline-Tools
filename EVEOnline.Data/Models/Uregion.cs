﻿using System;
using System.Collections.Generic;

namespace EVEOnline.Data.Models;

public partial class Uregion
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? DescriptionRu { get; set; }

    public virtual ICollection<Uconstellation> Uconstellations { get; } = new List<Uconstellation>();
}
