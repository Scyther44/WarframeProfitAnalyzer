using WarframeProfitAnalyzer.Servivces;

// This class is responsible for showing the console (user interface)
class Program
{
    static async Task Main()
    {
        Console.WriteLine("Enter Warframe set URL name (e.g., 'volt_prime_set'):");
        var slug = Console.ReadLine();

        var analyzer = new SetProfitAnalyzer();
        await analyzer.CalculateSetProfitAsync(slug);
    }
}