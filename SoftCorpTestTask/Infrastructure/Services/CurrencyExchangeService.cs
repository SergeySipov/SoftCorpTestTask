using Application.Interfaces.Services;
using Infrastructure.Constants;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using Application.Common.Models.CurrencyExchange;
using Domain.Enums;

namespace Infrastructure.Services;

public class CurrencyExchangeService : ICurrencyExchangeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public CurrencyExchangeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<CurrencyConverterResponseModel> ConvertCurrenciesAsync(CurrencyCode baseCurrency, CurrencyCode targetCurrency, decimal amount)
    {
        if (amount <= decimal.Zero)
        {
            throw new ArgumentException(null, nameof(amount));
        }

        var client = _httpClientFactory.CreateClient(HttpClientConstants.OpenExchangeRatesHttpClientName);
        
        var response = await client.GetAsync($"convert?to={targetCurrency}&from={baseCurrency}&amount={amount}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }

        var convertResponse = await response.Content.ReadFromJsonAsync<CurrencyConverterResponseModel>();

        if (convertResponse == null)
        {
            throw new SerializationException("can't read response");
        }

        return convertResponse;
    }
}
