using Application.UseCases.CostCategoryFeatures.Commands.Create;
using Application.UseCases.CostCategoryFeatures.Commands.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings.Profiles;

public class CostCategoryMappingsProfile : Profile 
{
    public CostCategoryMappingsProfile()
    {
        CreateMap<CreateCostCategoryCommand, CostCategory>();
        CreateMap<UpdateCostCategoryCommand, CostCategory>();
    }
}
