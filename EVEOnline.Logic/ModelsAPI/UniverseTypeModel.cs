using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EVEOnline.Logic.ModelsAPI
{
    public partial class UniverseTypeModel
    {
        public double? capacity { get; set; }
        public string description { get; set; }
        public DogmaAttributes[] dogma_attributes { get; set; }
        public DogmaEffects[] dogma_effects { get; set; }
        public int? graphic_id { get; set; }
        public int group_id { get; set; }
        public int? icon_id { get; set; }

        public int? market_group_id { get; set; }

        public double? mass { get; set; }
        public string name { get; set; }

        public double? packaged_volume { get; set; }

        public int? portion_size { get; set; }

        public bool published { get; set; }

        public double? radius { get; set; }

        public int type_id { get; set; }
        public double? volume { get; set; }
    }
}
