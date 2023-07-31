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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllFamiliesQuery());
        return Ok(response);
    }
    
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

    [HttpPut]
    public async Task<IActionResult> AddOrUpdate(AddOrUpdateUserToFamilyCommand model)
    {
        await _mediator.Send(model);
        return Ok();
    }

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