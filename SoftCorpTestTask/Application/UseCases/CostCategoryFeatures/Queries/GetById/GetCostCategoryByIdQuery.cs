using Application.Common.Models;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.UseCases.CostCategoryFeatures.Queries.GetById;

public record GetCostCategoryByIdQuery : IRequest<CostCategoryBriefModel>
{
    public int CostCategoryId { get; init; }
}

internal class GetCostCategoryByIdQueryHandler : IRequestHandler<GetCostCategoryByIdQuery, CostCategoryBriefModel>
{
    private readonly ICostCategoryRepository _costCategoryRepository;
    private readonly IMapper _mapper;

    public GetCostCategoryByIdQueryHandler(ICostCategoryRepository costCategoryRepository, 
        IMapper mapper)
    {
        _costCategoryRepository = costCategoryRepository;
        _mapper = mapper;
    }

    public async Task<CostCategoryBriefModel> Handle(GetCostCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var dataModel = await _costCategoryRepository.GetByIdAsync(request.CostCategoryId);
        return _mapper.Map<CostCategoryBriefModel>(dataModel);
    }
}