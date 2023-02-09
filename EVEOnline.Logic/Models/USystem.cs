using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.Models
{
    public partial class USystem
    {
        public int constellation_id { get; set; }
        public string name { get; set; }
        public Planets[]? planets { get; set; }
        public Position position { get; set; }  
        public string? security_class { get; set; }
        public double security_status { get; set; }
        public int? star_id { get; set; }
        public int[]? stargates { get; set; }
        public int[]? stations { get; set; }
        public int system_id { get;}
    }
}
