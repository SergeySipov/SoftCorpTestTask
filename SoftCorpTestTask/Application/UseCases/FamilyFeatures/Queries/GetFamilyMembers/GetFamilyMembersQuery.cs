using Application.Common.Models;
using Application.Common.Models.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.FamilyFeatures.Queries.GetFamilyMembers;

public record GetFamilyMembersQuery : IRequest<FamilyMembersModel>
{
    public int FamilyId { get; init; }
}

internal class GetFamilyMembersQueryHandler : IRequestHandler<GetFamilyMembersQuery, FamilyMembersModel>
{
    private readonly IUserFamilyRepository _userFamilyRepository;
    private readonly IBaseRepository<Family> _userFamilyBaseRepository;
    private readonly IMapper _mapper;

    public GetFamilyMembersQueryHandler(IUserFamilyRepository userFamilyRepository, 
        IBaseRepository<Family> userFamilyBaseRepository, 
        IMapper mapper)
    {
        _userFamilyRepository = userFamilyRepository;
        _userFamilyBaseRepository = userFamilyBaseRepository;
        _mapper = mapper;
    }

    public async Task<FamilyMembersModel> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken)
    {
        var family = await _userFamilyBaseRepository.GetByIdAsync(request.FamilyId);
        if (family == null)
        {
            throw new Exception();
        }

        var familyMembers = await _userFamilyRepository.GetFamilyMembersAsync(family.Id)
            .ToListAsync(cancellationToken);

        return new FamilyMembersModel
        {
            Title = family.Title,
            FamilyMembers = _mapper.Map<IEnumerable<UserBriefModel>>(familyMembers).ToList()
        };
    }
}