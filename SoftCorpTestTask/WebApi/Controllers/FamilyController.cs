using Application.UseCases.FamilyFeatures.Commands.AddUserToFamily;
using Application.UseCases.FamilyFeatures.Commands.Create;
using Application.UseCases.FamilyFeatures.Commands.Delete;
using Application.UseCases.FamilyFeatures.Queries.GetAll;
using Application.UseCases.FamilyFeatures.Queries.GetById;
using Application.UseCases.FamilyFeatures.Queries.GetFamilyMembers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
public class FamilyController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public FamilyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all families
    /// </summary>
    /// <returns>List of families (id, title)</returns>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllFamiliesQuery());
        return Ok(response);
    }

    /// <summary>
    /// Get family by id
    /// </summary>
    /// <param name="familyId"></param>
    /// <returns>Returns one family entity</returns>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int familyId)
    {
        var query = new GetFamilyByIdQuery
        {
            Id = familyId
        };

        var response = await _mediator.Send(query);
        return Ok(response);
    }

    /// <summary>
    /// Create family
    /// </summary>
    /// <param name="familyTitle"></param>
    /// <returns></returns>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> Create(string familyTitle)
    {
        var query = new CreateFamilyCommand
        {
            Title = familyTitle
        };

        await _mediator.Send(query);
        return Ok();
    }

    /// <summary>
    /// Get list of users that belong to family
    /// </summary>
    /// <param name="familyId"></param>
    /// <returns>List of users</returns>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<IActionResult> GetFamilyMembers([FromQuery] int familyId)
    {
        var query = new GetFamilyMembersQuery
        {
            FamilyId = familyId
        };

        var response = await _mediator.Send(query);
        return Ok(response);
    }

    /// <summary>
    /// Add or update user family info
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="200">If create/update succeeded</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPut]
    public async Task<IActionResult> AddOrUpdate(AddOrUpdateUserToFamilyCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

    /// <summary>
    /// Delete cost category
    /// </summary>
    /// <param name="familyId"></param>
    /// <returns></returns>
    /// <response code="200">If delete operation succeeded</response>
    /// <response code="500">If something going wrong</response>
    /// <response code="400">If values that you have tried to pass are incorrect</response>
    /// <response code="401">If you are not authenticate</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int familyId)
    {
        var query = new DeleteFamilyCommand
        {
            FamilyId = familyId
        };

        await _mediator.Send(query);
        return Ok();
    }
}