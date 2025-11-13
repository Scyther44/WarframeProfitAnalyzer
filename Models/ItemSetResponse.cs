namespace WarframeProfitAnalyzer.Models
{
    public class ItemSetResponse
    {
        public SetData? Data { get; set; }
        public string? ApiVersion { get; set; }
        public object? Error { get; set; }
    }

    public class SetData
    {
        public List<Item>? Items { get; set; }
    }

    public class Item
    {
        public string? Id { get; set; }
        public string? Slug { get; set; }
        public bool SetRoot { get; set; }
        public int? QuantityInSet { get; set; }
        public ItemI18n? I18n { get; set; }

        // Easy access to English name
        public string Name => I18n?.En?.Name ?? Slug;
    }

    public class ItemI18n
    {
        public ItemLang? En { get; set; }
    }

    public class ItemLang
    {
        public string? Name { get; set; }
    }
}
