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

    /// <summary>
    /// Create cost category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="200">If cost category created successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCostCategoryCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

    /// <summary>
    /// Delete cost category (only if cost category is not in use)
    /// </summary>
    /// <param name="costCategoryId"></param>
    /// <returns></returns>
    /// <response code="200">If cost category deleted successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    /// <response code="403">If cost category in use</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int costCategoryId)
    {
        await _mediator.Send(new DeleteCostCategoryCommand
        {
            CostCategoryId = costCategoryId
        });

        return Ok();
    }

    /// <summary>
    /// Update cost category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="200">If cost category created successfully</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateCostCategoryCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

    /// <summary>
    /// Get all cost categories
    /// </summary>
    /// <returns></returns>
    /// <response code="500">If something going wrong</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllCostCategoriesQuery());
        return Ok(response);
    }

    /// <summary>
    /// Get cost category by id
    /// </summary>
    /// <param name="costCategoryId"></param>
    /// <returns>Cost category</returns>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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