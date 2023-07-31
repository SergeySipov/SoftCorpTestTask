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


    [HttpPost]
    public async Task<IActionResult> Create(CreateUsersCostCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

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

    [HttpPost]
    public async Task<IActionResult> Update(UpdateUsersCostCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> GetByMonth(GetAllUsersCostsByMonthQuery model)
    {
        var response = await _mediator.Send(model);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> GetByPeriod(GetFamilyCostsStatisticByPeriodQuery model)
    {
        model.UserId ??= UserId;
        var response = await _mediator.Send(model);

        return Ok(response);
    }
}