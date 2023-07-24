using Application.Common.Models.UsersCostStatistic;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Application.UseCases.UsersCostFeatures.Queries.Common;
using Application.Common.Models.DataModels;

namespace Application.UseCases.UsersCostFeatures.Queries.GetAllUsersCostsStatisticByPeriod;

public record GetFamilyCostsStatisticByPeriodQuery : UsersCostsSearchQueryBaseRequest<FamilyCostsStatisticModel>
{
    public DateTime StartPeriodDate { get; init; }
    public DateTime EndPeriodDate { get; init; }
}

internal class GetFamilyCostsStatisticByPeriodQueryHandler : IRequestHandler<GetFamilyCostsStatisticByPeriodQuery, FamilyCostsStatisticModel>
{
    private readonly IMapper _mapper;
    private readonly IUsersCostRepository _usersCostRepository;

    public GetFamilyCostsStatisticByPeriodQueryHandler(IMapper mapper, IUsersCostRepository usersCostRepository)
    {
        _mapper = mapper;
        _usersCostRepository = usersCostRepository;
    }

    public async Task<FamilyCostsStatisticModel> Handle(GetFamilyCostsStatisticByPeriodQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new UsersCostsSearchDataModel
        {
            UserId = request.UserId,
            CostCategoriesIds = request.CostCategoriesIds,
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        };

        var familyCostStatisticDataModel = await _usersCostRepository.GetFamilyCostsStatisticByPeriodAsync(searchModel, 
            request.StartPeriodDate,
            request.EndPeriodDate);

        var familyCostModel = _mapper.Map<FamilyCostsStatisticModel>(familyCostStatisticDataModel);
        return familyCostModel;
    }
}