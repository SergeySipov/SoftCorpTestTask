using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.CostCategoryFeatures.Commands.Create;

public record CreateCostCategoryCommand : IRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Color { get; init; }
}

internal class CreateCostCategoryCommandHandler : IRequestHandler<CreateCostCategoryCommand>
{
    private readonly IBaseRepository<CostCategory> _costCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCostCategoryCommandHandler(IBaseRepository<CostCategory> costCategoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _costCategoryRepository = costCategoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task Handle(CreateCostCategoryCommand request, CancellationToken cancellationToken)
    {
        var costCategoryEntity = _mapper.Map<CostCategory>(request);
        _costCategoryRepository.Add(costCategoryEntity);
        
        return _unitOfWork.SaveAsync(cancellationToken);
    }
}