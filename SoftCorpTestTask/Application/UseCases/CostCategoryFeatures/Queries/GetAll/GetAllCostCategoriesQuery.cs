using Application.Common.Models;
using Application.Interfaces.Repositories.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.CostCategoryFeatures.Queries.GetAll;

public record GetAllCostCategoriesQuery : IRequest<List<CostCategoryModel>>;

internal class GetAllCostCategoriesQueryHandler : IRequestHandler<GetAllCostCategoriesQuery, List<CostCategoryModel>>
{
    private readonly IBaseRepository<CostCategory> _costCategoryBaseRepository;
    private readonly IMapper _mapper;

    public GetAllCostCategoriesQueryHandler(IBaseRepository<CostCategory> costCategoryBaseRepository, 
        IMapper mapper)
    {
        _costCategoryBaseRepository = costCategoryBaseRepository;
        _mapper = mapper;
    }

    public async Task<List<CostCategoryModel>> Handle(GetAllCostCategoriesQuery request, CancellationToken cancellationToken)
    {
        var costCategoryEntities = await _costCategoryBaseRepository.GetAllAsync().ToListAsync(cancellationToken);
        return _mapper.Map<List<CostCategoryModel>>(costCategoryEntities);
    }
}
