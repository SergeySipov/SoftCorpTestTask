using Application.Common.Models;
using Application.Common.Models.Common;
using Application.Common.Models.DataModels;
using Application.Interfaces.Repositories.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UsersCostFeatures.Queries.GetAllUsersCostsByMonth;

public record GetAllUsersCostsByMonthQuery : IRequest<PaginatedList<UsersCostModel>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public IEnumerable<int> CostCategoriesIds { get; init; }
    public int UserId { get; init; }
}

internal class GetAllUsersCostsByMonthQueryHandler : IRequestHandler<GetAllUsersCostsByMonthQuery, PaginatedList<UsersCostModel>>
{
    private readonly IMapper _mapper;
    private readonly IPaginatedRepository<UsersCost> _usersCostRepository;

    public GetAllUsersCostsByMonthQueryHandler(IMapper mapper, 
        IPaginatedRepository<UsersCost> usersCostRepository)
    {
        _mapper = mapper;
        _usersCostRepository = usersCostRepository;
    }

    public async Task<PaginatedList<UsersCostModel>> Handle(GetAllUsersCostsByMonthQuery request, CancellationToken cancellationToken)
    {
        var usersCostDataModels = await _usersCostRepository
            .GetPaginatedChunkAsync<UsersCostDataModel>(request.PageNumber, request.PageSize)
            .ToListAsync(cancellationToken);
        
        var usersCostModels = _mapper.Map<List<UsersCostModel>>(usersCostDataModels);
        var totalItemsCount = await _usersCostRepository.GetTotalNumberOfEntitiesAsync(cancellationToken);

        var paginatedModel = new PaginatedList<UsersCostModel>(usersCostModels,
            totalItemsCount,
            request.PageNumber,
            request.PageSize);

        return paginatedModel;
    }
}