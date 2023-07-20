using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.CostCategoryFeatures.Commands.Update;

public record UpdateCostCategoryCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
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
    
    public Task Handle(UpdateCostCategoryCommand request, CancellationToken cancellationToken)
    {//TODO проверка на наличие сущности
        var costCategoryToUpdate = _mapper.Map<CostCategory>(request);
        _costCategoryBaseRepository.Update(costCategoryToUpdate);
        
        return _unitOfWork.SaveAsync(cancellationToken);
    }
}