namespace WarframeProfitAnalyzer.Models
{
    internal class SetInfo
    {
        public class Rootobject
        {
            public Payload? Payload { get; set; }
        }

        public class Payload
        {
            public Item? Item { get; set; }
        }

        public class Item
        {
            public string? id { get; set; }
            public Items_In_Set[]? items_In_Set { get; set; }
        }

        public class Items_In_Set
        {
            public string? url_name { get; set; }
            public int? quantity_for_set { get; set; }
        }
    }
}
