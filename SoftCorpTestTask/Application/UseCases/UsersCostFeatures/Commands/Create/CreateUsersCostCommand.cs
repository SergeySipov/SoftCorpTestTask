using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UsersCostFeatures.Commands.Create;

public record CreateUsersCostCommand : IRequest
{
    public decimal Price { get; init; }
    public string Comment { get; init; }
    public DateTime Date { get; init; }
    public int UserId { get; init; }
    public int CostId { get; init; }
}

internal class CreateUsersCostCommandHandler : IRequestHandler<CreateUsersCostCommand>
{
    private readonly IBaseRepository<UsersCost> _usersCostRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public CreateUsersCostCommandHandler(IBaseRepository<UsersCost> usersCostRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _usersCostRepository = usersCostRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task Handle(CreateUsersCostCommand request, CancellationToken cancellationToken)
    {
        var usersCostEntity = _mapper.Map<UsersCost>(request);
        _usersCostRepository.Add(usersCostEntity);

        return _unitOfWork.SaveAsync(cancellationToken);
    }
}