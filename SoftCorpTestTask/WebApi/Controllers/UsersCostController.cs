using Application.UseCases.UsersCostFeatures.Commands.Create;
using Application.UseCases.UsersCostFeatures.Commands.Delete;
using Application.UseCases.UsersCostFeatures.Commands.Update;
using Application.UseCases.UsersCostFeatures.Queries.GetAllUsersCostsByMonth;
using Application.UseCases.UsersCostFeatures.Queries.GetAllUsersCostsStatisticByPeriod;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
public class UsersCostController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public UsersCostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create user's cost
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="200">If user's cost created successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUsersCostCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

    /// <summary>
    /// Delete user's cost
    /// </summary>
    /// <param name="currentUserId"></param>
    /// <param name="usersCostId"></param>
    /// <returns></returns>
    /// <response code="200">If user's cost deleted successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int currentUserId, int usersCostId)
    {
        await _mediator.Send(new DeleteUsersCostCommand
        {
            CurrentUserId = currentUserId,
            UsersCostId = usersCostId
        });

        return Ok();
    }

    /// <summary>
    /// Update user's cost
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="200">If user's cost updated successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> Update(UpdateUsersCostCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

    /// <summary>
    /// Get paginated list of user's costs. Accepts PageNumber and PageSize, as well as two optional filters by user and category
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Pagination info and chunk of user's costs</returns>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> GetByMonth(GetAllUsersCostsByMonthQuery model)
    {
        var response = await _mediator.Send(model);
        return Ok(response);
    }

    /// <summary>
    /// Get paginated list of user's costs for all family members. For every family member you can configure pagination
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Pagination info and chunk of user's costs for all family members</returns>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> GetByPeriod(GetFamilyCostsStatisticByPeriodQuery model)
    {
        model.UserId ??= UserId;
        var response = await _mediator.Send(model);

        return Ok(response);
    }
}