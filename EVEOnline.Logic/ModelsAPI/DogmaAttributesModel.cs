using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EVEOnline.Logic.ModelsAPI
{
    public partial class DogmaAttributesModel
    {
        public int attribute_id { get; set; }
        public float? default_value { get; set; }
        public string? description { get; set; }
        public string? display_name { get; set; }
        public bool? high_is_good { get; set; }
        public int? icon_id { get; set; }
        public string? name { get; set; }
        public bool? published { get; set; }
        public bool? stackable { get; set; }
        public int? unit_id { get; set; }

    }
}
