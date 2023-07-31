using Application.Common.Models;
using Application.Common.Models.Common;
using Application.Common.Models.DataModels;
using Application.Common.Models.DataModels.UsersCostStatistic;
using Application.Common.Models.UsersCostStatistic;
using Application.UseCases.UsersCostFeatures.Commands.Create;
using Application.UseCases.UsersCostFeatures.Commands.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings.Profiles;

public class UsersCostMappingsProfile : Profile
{
    public UsersCostMappingsProfile()
    {
        CreateMap<CreateUsersCostCommand, UsersCost>();
        CreateMap<UpdateUsersCostCommand, UsersCost>();
        CreateMap<UsersCostDataModel, UsersCostModel>();
        CreateMap<PaginatedList<UsersCostDataModel>, PaginatedList<UsersCostModel>>();

        CreateMap<FamilyCostsStatisticDataModel, FamilyCostsStatisticModel>();
        CreateMap<FamilyMemberCostsStatisticDataModel, FamilyMemberCostsStatisticModel>();
        CreateMap<UsersCostsStatisticDataModel, UsersCostsStatisticModel>();
        CreateMap<PaginatedList<UsersCostsStatisticDataModel>, PaginatedList<UsersCostsStatisticModel>>();
    }
}