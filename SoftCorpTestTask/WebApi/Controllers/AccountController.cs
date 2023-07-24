using Application.UseCases.UserFeatures.Commands.AuthorizeUserAndCreateTokens;
using Application.UseCases.UserFeatures.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[AllowAnonymous]
public class AccountController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand model, CancellationToken token)
    {
        await _mediator.Send(model, token);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(AuthenticateUserAndGetJwtTokenCommand model, CancellationToken token)
    {
        var response = await _mediator.Send(model, token);
        return Ok(response);
    }
}