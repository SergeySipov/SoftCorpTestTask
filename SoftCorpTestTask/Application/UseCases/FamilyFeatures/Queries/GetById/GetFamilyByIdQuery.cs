using Application.Common.Models;
using Application.Interfaces.Repositories.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.FamilyFeatures.Queries.GetById;

public record GetFamilyByIdQuery : IRequest<FamilyModel>
{
    public int Id { get; set; }
}

internal class GetFamilyByIdQueryHandler : IRequestHandler<GetFamilyByIdQuery, FamilyModel>
{
    private readonly IBaseRepository<Family> _familyBaseRepository;
    private readonly IMapper _mapper;

    public GetFamilyByIdQueryHandler(IBaseRepository<Family> familyBaseRepository, 
        IMapper mapper)
    {
        _familyBaseRepository = familyBaseRepository;
        _mapper = mapper;
    }

    public async Task<FamilyModel> Handle(GetFamilyByIdQuery request, CancellationToken cancellationToken)
    {
        var family = await _familyBaseRepository.GetByIdAsync(request.Id);
        return _mapper.Map<FamilyModel>(family);
    }
}