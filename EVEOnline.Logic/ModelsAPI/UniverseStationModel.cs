using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EVEOnline.Logic.ModelsAPI
{
    public partial class UniverseStationModel
    {
        public float max_dockable_ship_volume {get;set;}
        public string name { get;set;}
        public float office_rental_cost { get;set;}
        public int? owner { get; set; } //ID of the corporation that controls this station
        public Position position { get; set; }
        public int? race_id { get; set; }
        public decimal reprocessing_efficiency { get; set; }
        public decimal reprocessing_stations_take { get; set; }
        public string[] services { get; set; }
        public long station_id { get; set; }
        public int system_id { get; set; }
        public int type_id { get; set; }
    }
}
