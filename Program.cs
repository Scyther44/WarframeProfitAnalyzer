using System.Diagnostics;
using WarframeProfitAnalyzer.Servivces;

// This class is responsible for showing the console (user interface)
class Program
{
    static async Task Main()
    {
        var analyzer = new SetProfitAnalyzer();
        while (true)
        {
            Console.WriteLine("Enter Warframe set URL name (e.g., 'volt_prime_set') or use 'all' for full analysis:");
            var slug = Console.ReadLine();
            if (slug.ToLower().Equals("all"))
            {
                Console.WriteLine("Fetching all sets...");
                var stopwatch = Stopwatch.StartNew();
                var sets = await SetProfitAnalyzer.GetAllSetsAsync();
                var profitableSets = new List<SetProfitResult>();

                Console.WriteLine($"Found {sets.Count} tradable sets.");

                foreach (var set in sets)
                {
                    (int setCost, int partsCost) = await analyzer.CalculateSetProfitAsync(set.Slug);

                    var profit = setCost - partsCost;

                    if (profit >= 10) // min threshold
                        profitableSets.Add(new SetProfitResult(set.Name, setCost, partsCost, profit));
                }
                stopwatch.Stop();
                Console.WriteLine($"\n--- Profitable Sets (Profit >= 10) ---");
                foreach (var result in profitableSets.OrderByDescending(r => r.Profit))
                {
                    Console.WriteLine($"{result.Name}: Profit {result.Profit}, Set cost {result.SetCost}, Parts cost {result.PartsCost}");
                }
                Console.WriteLine($"Total time: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");

            }
            else if (!string.IsNullOrWhiteSpace(slug))
            {
                await analyzer.CalculateSetProfitAsync(slug);
            }
        }
    }
}

public record SetProfitResult(string Name, int SetCost, int PartsCost, int Profit);