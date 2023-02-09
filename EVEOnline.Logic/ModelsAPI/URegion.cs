using System;
using System.Collections.Generic;

namespace EVEOnline.Logic.Models;

public partial class URegion
{
    public int[] constellations { get; set; }
    public string? description { get; set; }
    public string name { get; set; }
    public int nameregion_id { get; set; }
}