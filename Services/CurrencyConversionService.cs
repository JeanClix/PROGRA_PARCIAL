using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PROGRA_PARCIAL.Services
{
    public interface ICurrencyConversionService
    {
        Task<decimal> GetExchangeRateAsync(string from, string to);
    }

    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly HttpClient _httpClient;

        public CurrencyConversionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetExchangeRateAsync(string from, string to)
        {
            var response = await _httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(content);

            if (from.ToUpper() == "USD" && to.ToUpper() == "BTC")
            {
                return 1 / data["bitcoin"]["usd"];
            }
            else if (from.ToUpper() == "BTC" && to.ToUpper() == "USD")
            {
                return data["bitcoin"]["usd"];
            }
            else
            {
                throw new ArgumentException("Conversión no soportada");
            }
        }
    }
}