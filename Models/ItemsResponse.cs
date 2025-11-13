using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeProfitAnalyzer.Models
{
    public class ItemsResponse
    {
        public string? ApiVersion { get; set; }
        public List<ItemShort>? Data { get; set; }
        public object? Error { get; set; }
    }

    public class ItemShort
    {
        public string? Id { get; set; }
        public string? Slug { get; set; }
        public List<string>? Tags { get; set; }
        public ItemI18n? I18n { get; set; }
        public string Name => I18n?.En?.Name ?? Slug;
    }
}
