using FluentValidation;

namespace Application.UseCases.UsersCostFeatures.Queries.GetAllUsersCostsByMonth;

public class GetAllUsersCostsByMonthQueryValidator : AbstractValidator<GetAllUsersCostsByMonthQuery>
{
    public GetAllUsersCostsByMonthQueryValidator()
    {
        RuleFor(g => g.Month)
            .IsInEnum();
    }
}
