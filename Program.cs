using WarframeProfitAnalyzer.Servivces;

// This class is responsible for showing the console (user interface)
class Program
{
    static async Task Main()
    {
        var analyzer = new SetProfitAnalyzer();
        while (true)
        {
            Console.WriteLine("Enter Warframe set URL name (e.g., 'volt_prime_set'):");
            var slug = Console.ReadLine();
            await analyzer.CalculateSetProfitAsync(slug);
        }
    }
}