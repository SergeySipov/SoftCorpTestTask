using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.CostCategoryFeatures.Commands.Delete;

public record DeleteCostCategoryCommand : IRequest
{
    public int CostCategoryId { get; init; }
}

internal class DeleteCostCategoryCommandHandler : IRequestHandler<DeleteCostCategoryCommand>
{
    private readonly IBaseRepository<CostCategory> _costCategoryBaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICostCategoryRepository _costCategoryRepository;

    public DeleteCostCategoryCommandHandler(IBaseRepository<CostCategory> costBaseCategoryRepository,
        IUnitOfWork unitOfWork, 
        ICostCategoryRepository costCategoryRepository)
    {
        _costCategoryBaseRepository = costBaseCategoryRepository;
        _unitOfWork = unitOfWork;
        _costCategoryRepository = costCategoryRepository;
    }
    
    public async Task Handle(DeleteCostCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCostCategoryInUse = await _costCategoryRepository.IsCostCategoryInUse(request.CostCategoryId);
        if (isCostCategoryInUse)
        {
            throw new InvalidOperationException("It's impossible to delete the category cause it's in use");
        }

        _costCategoryBaseRepository.Delete(request.CostCategoryId);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}