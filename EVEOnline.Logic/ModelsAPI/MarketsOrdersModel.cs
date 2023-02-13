using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EVEOnline.Logic.ModelsAPI
{
    public partial class MarketsOrdersModel
    {
        public int duration { get; set; }
        public bool is_buy_order { get; set; }
        public DateTime issued { get; set; }
        public long location_id { get; set; }
        public int min_volume { get; set; }
        public long order_id { get; set; }
        public decimal price { get; set; } //возможно надо разделить на 100
        public string range { get; set; } //[ station, region, solarsystem, 1, 2, 3, 4, 5, 10, 20, 30, 40 ]
        public int system_id { get; set; } //В Солнечной системе этот порядок был размещен
        public int type_id { get; set; }
        public int volume_remain { get; set; } //Сколько осталось
        public int volume_total { get; set; } //Сколько всего
    }
}
