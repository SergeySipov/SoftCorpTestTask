using Application.UseCases.UserFeatures.Commands.AuthorizeUserAndCreateTokens;
using Application.UseCases.UserFeatures.Commands.Create;
using Application.UseCases.UserFeatures.Commands.RefreshAccessToken;
using Application.UseCases.UserFeatures.Commands.RevokeAccessToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AccountController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand model, CancellationToken token)
    {
        await _mediator.Send(model, token);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(AuthenticateUserAndGetJwtTokenCommand model, CancellationToken token)
    {
        var response = await _mediator.Send(model, token);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> RefreshToken(RefreshAccessTokenCommand model, CancellationToken token)
    {
        var response = await _mediator.Send(model, token);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Revoke(string refreshToken, CancellationToken token)
    {
        await _mediator.Send(new RevokeAccessTokenCommand
        {
            RefreshToken = refreshToken
        }, token);

        return Ok();
    }
}