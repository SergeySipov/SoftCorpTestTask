using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.FamilyFeatures.Commands.AddUserToFamily;

public record AddOrUpdateUserToFamilyCommand : IRequest
{
    public int UserId { get; init; }
    public int FamilyId { get; init; }
}

internal class AddOrUpdateUserToFamilyCommandHandler : IRequestHandler<AddOrUpdateUserToFamilyCommand>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<UserFamily> _userFamilyBaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserFamilyRepository _userFamilyRepository;

    public AddOrUpdateUserToFamilyCommandHandler(IMapper mapper, 
        IBaseRepository<UserFamily> userFamilyBaseRepository, 
        IUnitOfWork unitOfWork, 
        IUserFamilyRepository userFamilyRepository)
    {
        _mapper = mapper;
        _userFamilyBaseRepository = userFamilyBaseRepository;
        _unitOfWork = unitOfWork;
        _userFamilyRepository = userFamilyRepository;
    }

    public async Task Handle(AddOrUpdateUserToFamilyCommand request, CancellationToken cancellationToken)
    {
        var existedUserFamily = await _userFamilyRepository.GetAsync(request.UserId, request.FamilyId);
        if (existedUserFamily == null)
        {
            existedUserFamily = _mapper.Map<UserFamily>(request);
            _userFamilyBaseRepository.Add(existedUserFamily);
            await _unitOfWork.SaveAsync(cancellationToken);
            return;
        }

        existedUserFamily.FamilyId = request.FamilyId;
        existedUserFamily.UserId = request.UserId;
        await _userFamilyBaseRepository.UpdateAsync(existedUserFamily);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}