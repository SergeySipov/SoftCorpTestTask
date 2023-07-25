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

    [HttpPost]
    public async Task<IActionResult> GetCurrency(GetCurrentExchangeRateQuery query)
    {
        var exchangeRate = await _mediator.Send(query);

        return Ok(exchangeRate);
    }
}