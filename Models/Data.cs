using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeProfitAnalyzer.Models
{
    internal class Data
    {
        public class Rootobject
        {
            public Payload? Payload { get; set; }
        }

        public class Payload
        {
            public Items[]? Items { get; set; }
        }

        public class Items
        {
            public string? Item_Name { get; set; }
            public string? url_name { get; set; }
        }
    }
}
