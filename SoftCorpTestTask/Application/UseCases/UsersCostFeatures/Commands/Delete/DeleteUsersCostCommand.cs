using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UsersCostFeatures.Commands.Delete;

public record DeleteUsersCostCommand : IRequest
{
    public int UsersCostId { get; set; }
    public int CurrentUserId { get; set; }
}

internal class DeleteUsersCostCommandHandler : IRequestHandler<DeleteUsersCostCommand>
{
    private readonly IBaseRepository<UsersCost> _usersCostBaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteUsersCostCommandHandler(IBaseRepository<UsersCost> usersCostBaseRepository, 
        IUnitOfWork unitOfWork)
    {
        _usersCostBaseRepository = usersCostBaseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteUsersCostCommand request, CancellationToken cancellationToken)
    {
        var usersCostEntity = await _usersCostBaseRepository.GetByIdAsync(request.UsersCostId);
        if (usersCostEntity.UserId == request.CurrentUserId)
        {
            throw new InvalidOperationException("An entry you create cannot be deleted or edited");
        }

        _usersCostBaseRepository.Delete(usersCostEntity);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}