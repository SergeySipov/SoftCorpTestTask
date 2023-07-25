using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.UsersCostFeatures.Commands.Update;

public record UpdateUsersCostCommand : IRequest
{
    public int Id { get; init; }
    public decimal Price { get; init; }
    public CurrencyCode CurrencyCode { get; init; }
    public string Comment { get; init; }
    public DateTime Date { get; init; }
    public int UserId { get; init; }
    public int CostId { get; init; }
    public int CurrentUserId { get; init; }
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
        await _usersCostBaseRepository.UpdateAsync(usersCostToUpdate);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}