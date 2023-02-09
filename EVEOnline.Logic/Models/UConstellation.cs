using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.Models
{
    public partial class UConstellation
    {
        public int constellation_id { get; set; }
        public string name { get; set; }
        public Position position { get; set; }
        public int region_id { get; set; }
        public int[] systems { get; set;}
    }
}
