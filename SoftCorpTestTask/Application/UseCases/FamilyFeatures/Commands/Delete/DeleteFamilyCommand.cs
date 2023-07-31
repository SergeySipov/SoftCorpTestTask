using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.FamilyFeatures.Commands.Delete;

public record DeleteFamilyCommand : IRequest
{
    public int FamilyId { get; init; }
}

internal class DeleteFamilyCommandHandler : IRequestHandler<DeleteFamilyCommand>
{
    private readonly IBaseRepository<Family> _familyBaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFamilyCommandHandler(IBaseRepository<Family> familyBaseRepository, 
        IUnitOfWork unitOfWork)
    {
        _familyBaseRepository = familyBaseRepository;
        _unitOfWork = unitOfWork;
    }

    public Task Handle(DeleteFamilyCommand request, CancellationToken cancellationToken)
    {
        _familyBaseRepository.Delete(request.FamilyId);
        return _unitOfWork.SaveAsync(cancellationToken);
    }
}