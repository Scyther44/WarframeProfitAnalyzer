using WarframeProfitAnalyzer.Models;
using WarframeProfitAnalyzer.Services;

namespace WarframeProfitAnalyzer.Servivces
{
    // This class uses WarframeMarketApi to calculate set profit and print live console output
    public class SetProfitAnalyzer
    {
        public async Task<(int setCost, int partsCost)> CalculateSetProfitAsync(string setSlug)
        {
            ItemSetResponse? setResponse = null;
            try
            {
                setResponse = await WarframeMarketApi.GetAsync<ItemSetResponse>($"item/{setSlug}/set");
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Invalid HTTP Request: Could not reach Warframe Market. Please use a correct url. Make sure your internet and warframe market are working.\n");
                return (0, 0);
            }
            catch (System.Text.Json.JsonException)
            {
                Console.WriteLine("API returned unexpected data. The item may not exist.\n");
                return (0, 0);
            }

            var setItems = setResponse?.Data?.Items;

            if (setItems == null || setItems.Count == 0)
            {
                Console.WriteLine($"Skipping {setSlug} (no items found).");
                return (0, 0);
            }

            int setCost = 0;
            int partsCost = 0;

            Console.WriteLine($"Fetching data for set: {setSlug} ...\n");

            foreach (var item in setItems)
            {
                Console.WriteLine($"Fetching price for item: {item.Name} ...");

                var ordersResponse = await WarframeMarketApi.GetAsync<OrdersResponse>($"orders/item/{item.Slug}/top");

                var sellOrders = ordersResponse?.Data?.Sell?
                    .OrderBy(o => o.Platinum)
                    .ToList();

                if (sellOrders == null || sellOrders.Count == 0)
                {
                    Console.WriteLine($"No sell orders found for {item.Name}, skipping.");
                    continue;
                }

                int requiredQuantity = item.QuantityInSet ?? 1;
                int totalPrice = GetTotalPriceForQuantity(sellOrders, requiredQuantity);

                if (item.Slug.EndsWith("set"))
                {
                    setCost = totalPrice;
                    Console.WriteLine($"Set price: {setCost} platinum\n");
                }
                else
                {
                    partsCost += totalPrice;
                    Console.WriteLine($"{item.Name}: {sellOrders[0].Platinum} platinum x{requiredQuantity} = {totalPrice}");
                }

                await Task.Delay(1000); // rate limiting
            }

            Console.WriteLine($"\nSet cost: {setCost}");
            Console.WriteLine($"Parts cost: {partsCost}");
            Console.WriteLine($"Profit: {setCost - partsCost}\n");

            return (setCost, partsCost);
        }

        private int GetTotalPriceForQuantity(List<Order>? sellOrders, int requiredQuantity)
        {
            if (sellOrders == null || sellOrders.Count == 0)
                return 0;

            int totalBought = 0;
            int totalCost = 0;

            foreach (var order in sellOrders)
            {
                if (order == null) continue;

                int available = order.Quantity;
                int take = Math.Min(requiredQuantity - totalBought, available);
                totalCost += take * order.Platinum;
                totalBought += take;

                if (totalBought >= requiredQuantity)
                    break;
            }

            return totalCost;
        }
    }
}
