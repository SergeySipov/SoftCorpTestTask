using Application.Interfaces.Services;
using MediatR;
using Domain.Enums;

namespace Application.UseCases.CurrencyFeatures.Queries.GetCurrentExchangeRate;

public record GetCurrentExchangeRateQuery : IRequest<double>
{
    public string FromCurrencyCode { get; set; }
    public string ToCurrencyCode { get; set; }
}

internal class GetCurrentExchangeRateQueryHandler : IRequestHandler<GetCurrentExchangeRateQuery, double>
{
    private readonly ICurrencyExchangeService _currencyExchangeService;

    public GetCurrentExchangeRateQueryHandler(ICurrencyExchangeService currencyExchangeService)
    {
        _currencyExchangeService = currencyExchangeService;
    }

    public async Task<double> Handle(GetCurrentExchangeRateQuery request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(typeof(CurrencyCode), request.FromCurrencyCode, out var fromCurrencyCode))
        {
            throw new ArgumentException();
        }

        if (!Enum.TryParse(typeof(CurrencyCode), request.ToCurrencyCode, out var toCurrencyCode))
        {
            throw new ArgumentException();
        }

        var response = await _currencyExchangeService.ConvertCurrenciesAsync(
            (CurrencyCode)fromCurrencyCode!, (CurrencyCode)toCurrencyCode!, 1);

        return response.result;
    }
}