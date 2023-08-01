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

    /// <summary>
    /// Creates a user entity used for user authentication in the system
    /// </summary>
    /// <param name="model"></param>
    /// <returns>empty</returns>
    /// <remarks>
    /// sample query:
    /// 
    /// curl -X 'POST' \
    ///     'https://localhost:5000/api/Account/Create' \
    ///     -H 'accept: */*' \
    ///     -H 'Content-Type: application/json' \
    ///     -d '{
    ///     "email": "string",
    ///     "password": "string",
    ///     "username": "string",
    ///     "firstName": "string",
    ///     "lastName": "string"
    /// }'
    ///
    /// </remarks>
    /// <response code="200">If user created successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand model, CancellationToken token)
    {
        await _mediator.Send(model, token);
        return Ok();
    }

    /// <summary>
    /// Get user's email and password and if an entity with the specified data exists returns Username, AccessToken and RefreshToken
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Username, AccessToken, RefreshToken</returns>
    /// <response code="200">If user created successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(AuthenticateUserAndGetJwtTokenCommand model, CancellationToken token)
    {
        var response = await _mediator.Send(model, token);
        return Ok(response);
    }

    /// <summary>
    /// Accepts access and refresh token and outputs the updated ones
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Username, AccessToken, RefreshToken</returns>
    /// <response code="200">If user created successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> RefreshToken(RefreshAccessTokenCommand model, CancellationToken token)
    {
        var response = await _mediator.Send(model, token);
        return Ok(response);
    }

    /// <summary>
    /// Revoke user refresh token (AccessToken will continue to operate)
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    /// <response code="200">If user created successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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