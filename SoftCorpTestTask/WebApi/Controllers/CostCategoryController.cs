using Application.UseCases.CostCategoryFeatures.Commands.Create;
using Application.UseCases.CostCategoryFeatures.Commands.Delete;
using Application.UseCases.CostCategoryFeatures.Commands.Update;
using Application.UseCases.CostCategoryFeatures.Queries.GetAll;
using Application.UseCases.CostCategoryFeatures.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
public class CostCategoryController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public CostCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCostCategoryCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int costCategoryId)
    {
        await _mediator.Send(new DeleteCostCategoryCommand
        {
            CostCategoryId = costCategoryId
        });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCostCategoryCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllCostCategoriesQuery());
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int costCategoryId)
    {
        var response = await _mediator.Send(new GetCostCategoryByIdQuery
        {
            CostCategoryId = costCategoryId
        });

        return Ok(response);
    }
}