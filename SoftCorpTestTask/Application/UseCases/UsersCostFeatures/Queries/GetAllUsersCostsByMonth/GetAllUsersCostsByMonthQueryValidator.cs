using FluentValidation;

namespace Application.UseCases.UsersCostFeatures.Queries.GetAllUsersCostsByMonth;

public class GetAllUsersCostsByMonthQueryValidator : AbstractValidator<GetAllUsersCostsByMonthQuery>
{
    public GetAllUsersCostsByMonthQueryValidator()
    {
        RuleFor(g => g.PageNumber)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(g => g.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(g => g.UserId)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleForEach(g => g.CostCategoriesIds)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
    }
}
