using System.Net.Http.Json;

// This class is responsible for fetching and deserializing data from the Warframe Market API

namespace WarframeProfitAnalyzer.Services
{
    public static class WarframeMarketApi
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://api.warframe.market/v2/")
        };

        public static async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetFromJsonAsync<T>(endpoint);
            return response;
        }
    }
}
