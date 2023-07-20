using Application.Common.Models;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.CostCategoryFeatures.Queries.GetById;

public record GetCostCategoryByIdQuery : IRequest<CostCategoryBriefModel>
{
    public int CostCategoryId { get; set; }
}

internal class GetCostCategoryByIdQueryHandler : IRequestHandler<GetCostCategoryByIdQuery, CostCategoryBriefModel>
{
    private readonly ICostCategoryRepository _costCategoryRepository;

    public GetCostCategoryByIdQueryHandler(ICostCategoryRepository costCategoryRepository)
    {
        _costCategoryRepository = costCategoryRepository;
    }

    public Task<CostCategoryBriefModel> Handle(GetCostCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return _costCategoryRepository.GetByIdAsync(request.CostCategoryId);
    }
}