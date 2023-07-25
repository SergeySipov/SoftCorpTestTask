using Application.Common.Models.CurrencyExchange;
using Domain.Enums;

namespace Application.Interfaces.Services;

public interface ICurrencyExchangeService
{
    Task<CurrencyConverterResponseModel> ConvertCurrenciesAsync(CurrencyCode baseCurrency, CurrencyCode targetCurrency, decimal amount);
}