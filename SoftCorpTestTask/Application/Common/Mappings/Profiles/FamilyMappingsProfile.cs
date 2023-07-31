using Application.UseCases.FamilyFeatures.Commands.AddUserToFamily;
using Application.UseCases.FamilyFeatures.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings.Profiles;

public class FamilyMappingsProfile : Profile
{
    public FamilyMappingsProfile()
    {
        CreateMap<CreateFamilyCommand, Family>();
        CreateMap<AddOrUpdateUserToFamilyCommand, UserFamily>();
    }
}