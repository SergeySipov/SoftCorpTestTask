using Application.Common.Models;
using Application.Common.Models.Common;
using Application.Common.Models.DataModels;
using Application.Interfaces.Repositories;
using Application.UseCases.UsersCostFeatures.Queries.Common;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.UsersCostFeatures.Queries.GetAllUsersCostsByMonth;

public record GetAllUsersCostsByMonthQuery : UsersCostsSearchQueryBaseRequest<PaginatedList<UsersCostModel>>
{
    public Month Month { get; init; }
}

internal class GetAllUsersCostsByMonthQueryHandler : IRequestHandler<GetAllUsersCostsByMonthQuery, PaginatedList<UsersCostModel>>
{
    private readonly IMapper _mapper;
    private readonly IUsersCostRepository _usersCostRepository;

    public GetAllUsersCostsByMonthQueryHandler(IMapper mapper, 
        IUsersCostRepository usersCostRepository)
    {
        _mapper = mapper;
        _usersCostRepository = usersCostRepository;
    }

    public async Task<PaginatedList<UsersCostModel>> Handle(GetAllUsersCostsByMonthQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new UsersCostsSearchDataModel
        {
            UserId = request.UserId,
            CostCategoriesIds = request.CostCategoriesIds,
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        };

        var usersCostsDataModel = await _usersCostRepository.GetUsersCostsByMonthAsync(
            searchModel, request.Month);

        return _mapper.Map<PaginatedList<UsersCostModel>>(usersCostsDataModel);
    }
}