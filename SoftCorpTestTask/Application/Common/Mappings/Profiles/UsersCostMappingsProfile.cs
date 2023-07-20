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
    }
}
