using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PROGRA_PARCIAL.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private const string API_KEY = "dd062fc2e08a077094ccfbf5"; // Tu API key
        private const string BASE_URL = "https://v6.exchangerate-api.com/v6";

        public ExchangeRateService()
        {
            _httpClient = new HttpClient();
        }

        // Clase para mapear la respuesta de la API
        public class ExchangeRateResponse
        {
            public string result { get; set; }
            public string documentation { get; set; }
            public string terms_of_use { get; set; }
            public long time_last_update_unix { get; set; }
            public string time_last_update_utc { get; set; }
            public long time_next_update_unix { get; set; }
            public string time_next_update_utc { get; set; }
            public string base_code { get; set; }
            public Dictionary<string, decimal> conversion_rates { get; set; }
        }

        // Método para obtener el tipo de cambio
        public async Task<decimal> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            try
            {
                string url = $"{BASE_URL}/{API_KEY}/latest/{fromCurrency}";
                var response = await _httpClient.GetStringAsync(url);
                
                var exchangeRates = JsonSerializer.Deserialize<ExchangeRateResponse>(response);
                
                if (exchangeRates?.conversion_rates != null && 
                    exchangeRates.conversion_rates.ContainsKey(toCurrency))
                {
                    return exchangeRates.conversion_rates[toCurrency];
                }
                
                throw new Exception($"No se encontró tasa de cambio para {toCurrency}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tasa de cambio: {ex.Message}");
            }
        }

        // Método para convertir un monto de una moneda a otra
        public async Task<decimal> ConvertAmount(decimal amount, string fromCurrency, string toCurrency)
        {
            var rate = await GetExchangeRate(fromCurrency, toCurrency);
            return amount * rate;
        }
    }
}