using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UsersCostFeatures.Commands.Update;

public record UpdateUsersCostCommand : IRequest
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public int CostId { get; set; }
    public int CurrentUserId { get; set; }
}

internal class UpdateUsersCostCommandHandler : IRequestHandler<UpdateUsersCostCommand>
{
    private readonly IBaseRepository<UsersCost> _usersCostBaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUsersCostCommandHandler(IBaseRepository<UsersCost> usersCostBaseRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _usersCostBaseRepository = usersCostBaseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateUsersCostCommand request, CancellationToken cancellationToken)
    {
        var usersCostEntity = await _usersCostBaseRepository.GetByIdAsync(request.Id);
        if (usersCostEntity.UserId == request.CurrentUserId)
        {
            throw new InvalidOperationException("An entry you create cannot be deleted or edited");
        }

        var usersCostToUpdate = _mapper.Map<UsersCost>(request);
        _usersCostBaseRepository.Update(usersCostToUpdate);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}