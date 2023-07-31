using FluentValidation;

namespace Application.UseCases.CostCategoryFeatures.Queries.GetById;

public class GetCostCategoryByIdQueryValidator : AbstractValidator<GetCostCategoryByIdQuery>
{
    public GetCostCategoryByIdQueryValidator()
    {
        RuleFor(g => g.CostCategoryId)
            .GreaterThan(0);
    }
}