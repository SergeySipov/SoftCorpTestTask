using Application.UseCases.CurrencyFeatures.Queries.GetCurrentExchangeRate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
public class CurrencyController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Allows you to find out the conversion rate of one currency to another
    /// </summary>
    /// <param name="FromCurrencyCode"></param>
    /// <param name="ToCurrencyCode"></param>
    /// <remarks>
    ///     Accept next params:
    ///     USD (US Dollar)
    ///     EUR (Euro)
    ///     GBP (British Pound)
    ///     JPY (Japanese Yen)
    ///     CHF (Swiss Franc)
    ///     CNY (Chinese Yuan)
    ///     RUB (Russian Ruble)
    ///     SEK (Swedish Krona)
    ///     NOK (Norwegian Krone)
    ///     DKK (Danish Krone)
    ///     PLN (Polish Zloty)
    ///     BYN (Belarussian Ruble)
    /// </remarks>
    /// <returns>Current currency</returns>
    /// <response code="200">If user created successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> GetCurrency(GetCurrentExchangeRateQuery query)
    {
        var exchangeRate = await _mediator.Send(query);

        return Ok(exchangeRate);
    }
}