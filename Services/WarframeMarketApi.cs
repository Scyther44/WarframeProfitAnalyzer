using Newtonsoft.Json;

// This class is responsible for fetching and deserializing data from the Warframe Market API

namespace WarframeProfitAnalyzer.Servivces
{
    public static class WarframeMarketApi
    {
        private static readonly HttpClient _client = new()
        {
            BaseAddress = new Uri("https://api.warframe.market/v1/")
        };

        public static async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<T>(response)!;
        }
    }
}
