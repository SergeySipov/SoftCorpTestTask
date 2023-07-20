using FluentValidation;

namespace Application.UseCases.CostCategoryFeatures.Commands.Delete;

public class DeleteCostCategoryCommandValidator : AbstractValidator<DeleteCostCategoryCommand>
{
    public DeleteCostCategoryCommandValidator()
    {
        RuleFor(d => d.CostCategoryId)
            .GreaterThan(0);
    }
}
