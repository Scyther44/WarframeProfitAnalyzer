using WarframeProfitAnalyzer.Servivces;

// This class is responsible for showing the console (user interface)
class Program
{
    static async Task Main()
    {
        var analyzer = new SetProfitAnalyzer();

        Console.WriteLine("Enter Warframe set URL name (e.g., 'volt_prime_set'):");
        var setUrl = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(setUrl))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        //Console.WriteLine("Fetching data...");
        var (setCost, partsCost) = await analyzer.CalculateSetProfitAsync(setUrl);

        Console.WriteLine($"Set cost: {setCost}");
        Console.WriteLine($"Parts cost: {partsCost}");
        Console.WriteLine($"Profit: {setCost - partsCost}");
    }
}