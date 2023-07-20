using Application.UseCases.CostCategoryFeatures.Commands.Create;
using Application.UseCases.CostCategoryFeatures.Commands.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings.Profiles;

public class CostCategoryMappingsProfile : Profile 
{
    public CostCategoryMappingsProfile()
    {
        CreateMap<CreateCostCategoryCommand, CostCategory>()
            .ForMember(destination => destination.Color, option =>
                option.MapFrom(c => int.Parse(c.Color, System.Globalization.NumberStyles.HexNumber)));

        CreateMap<UpdateCostCategoryCommand, CostCategory>()
            .ForMember(destination => destination.Color, option =>
                option.MapFrom(c => int.Parse(c.Color, System.Globalization.NumberStyles.HexNumber)));
    }
}
