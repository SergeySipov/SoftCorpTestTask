using Application.Common.Models.User;
using Application.UseCases.UserFeatures.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings.Profiles;

public class UserMappingsProfile : Profile
{
    public UserMappingsProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<UserRefreshTokenModel, UserRefreshToken>();
    }
}
