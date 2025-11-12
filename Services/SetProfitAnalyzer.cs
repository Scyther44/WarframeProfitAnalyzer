using WarframeProfitAnalyzer.Models;

namespace WarframeProfitAnalyzer.Servivces
{
    // This class uses WarframeMarketApi to calculate set and part costs, then determine profit margins.
    public class SetProfitAnalyzer
    {
        public async Task<(int setCost, int partsCost)> CalculateSetProfitAsync(string setUrl)
        {            
            Console.WriteLine($"\nFetching data for set: {setUrl} ...");
            
            // Fetch set info
            var setData = await WarframeMarketApi.GetAsync<SetInfo.Rootobject>($"items/{setUrl}");
            var items = setData.Payload.Item.items_In_Set;

            // If no data found
            if (items == null || items.Length == 0)
            {
                Console.WriteLine($"Skipping {setUrl} (no parts found).");
                return (0, 0);
            }

            int setCost = 0;
            int partsCost = 0;

            // Iterate through items in set info
            foreach (var item in items)
            {               
                int quantity = item.quantity_for_set ?? 1;

                // Print what we are fetching
                if (item.url_name.EndsWith("_set"))
                    Console.WriteLine($"\nFetching price for set itself: {item.url_name}");
                else
                    Console.WriteLine($"Fetching price for part: {item.url_name} (x{quantity}) ...");

                // Fetch item orders
                var itemPage = await WarframeMarketApi.GetAsync<ItemPage.Rootobject>($"items/{item.url_name}/orders");
                var price = GetLowestSellPrice(itemPage);

                if (price == null)
                {
                    Console.WriteLine($"No sell orders found for {item.url_name}");
                    continue;
                }

                // Assign price
                if (item.url_name.EndsWith("_set"))
                {
                    setCost = price.Value;
                    Console.WriteLine($"Set price: {price.Value} platinum\n");
                }
                else
                {
                    partsCost += price.Value * quantity;
                    Console.WriteLine($"{item.url_name}: {price.Value} platinum x{quantity} = {price.Value * quantity}\n");
                }

                await Task.Delay(2000); // small pause to avoid API rate limits
            }

            return (setCost, partsCost);
        }

        private static int? GetLowestSellPrice(ItemPage.Rootobject itemPage)
        {
            var orders = itemPage.Payload?.Orders;

            if (orders == null || orders.Length == 0)
                return null;

            var sellOrders = orders
                .Where(o => o.order_type == "sell")
                .OrderBy(o => o.Platinum)
                .ToList();

            if (sellOrders.Count == 0)
                return null;

            // Prefer in-game sellers if possible, otherwise take the first
            var ingameOrder = sellOrders.FirstOrDefault(o => o.User?.Status == "ingame");
            return (int?)(ingameOrder?.Platinum ?? sellOrders.First().Platinum);
        }
    }
}
