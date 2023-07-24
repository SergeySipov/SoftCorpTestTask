using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.CostCategoryFeatures.Commands.Update;

public record UpdateCostCategoryCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Color { get; init; }
}

internal class UpdateCostCategoryCommandHandler : IRequestHandler<UpdateCostCategoryCommand>
{
    private readonly IBaseRepository<CostCategory> _costCategoryBaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCostCategoryCommandHandler(IBaseRepository<CostCategory> costCategoryBaseRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _costCategoryBaseRepository = costCategoryBaseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateCostCategoryCommand request, CancellationToken cancellationToken)
    {
        var costCategoryToUpdate = _mapper.Map<CostCategory>(request);
        await _costCategoryBaseRepository.UpdateAsync(costCategoryToUpdate);
        
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}