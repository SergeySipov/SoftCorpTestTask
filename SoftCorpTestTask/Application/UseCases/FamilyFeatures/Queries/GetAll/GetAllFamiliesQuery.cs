using Application.Common.Models;
using Application.Interfaces.Repositories.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.FamilyFeatures.Queries.GetAll;

public record GetAllFamiliesQuery : IRequest<List<FamilyModel>>
{
}

internal class GetAllFamiliesQueryHandler : IRequestHandler<GetAllFamiliesQuery, List<FamilyModel>>
{
    private readonly IBaseRepository<Family> _familyBaseRepository;
    private readonly IMapper _mapper;

    public GetAllFamiliesQueryHandler(IBaseRepository<Family> familyBaseRepository, 
        IMapper mapper)
    {
        _familyBaseRepository = familyBaseRepository;
        _mapper = mapper;
    }

    public async Task<List<FamilyModel>> Handle(GetAllFamiliesQuery request, CancellationToken cancellationToken)
    {
        var families = await _familyBaseRepository.GetAllAsync().ToListAsync(cancellationToken);
        return _mapper.Map<List<FamilyModel>>(families);
    }
}